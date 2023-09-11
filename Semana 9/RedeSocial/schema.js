const { type } = require('express/lib/response')
const {GraphQLObjectType, GraphQLInt, GraphQLSchema, GraphQLString, GraphQLNonNull, GraphQLList,  GraphQLDirective, DirectiveLocation} = require('graphql') // criando varios schemas
const axios = require('axios').default; // Importe a biblioteca axios para buscar dados assincronos no json-server
//const { makeExecutableSchema } = require('graphql-tools');
//tipagem para os user

const UserType = new GraphQLObjectType({ //GraphQLObjectType tipo genericco
    name: 'User',
    description: 'Representa os dados de um usuario',
    fields: ()=> ({
        id: {type: GraphQLNonNull(GraphQLString)}, 
        name:{type: GraphQLString},
        age: {type: GraphQLInt},
        profession: {type: GraphQLString},
        email: {type: GraphQLString}
    })
})
const users = [
    {id: 1, name: "Jose Andrade", age: 23, profession: "dev1", email: "ja@mail.com"},
    {id: 2, name: "Neusa Maria", age: 24, profession: "dev1",email: "nm@mail.com"},
    {id: 3, name: "Jose Saramago", age: 23, profession: "dev3", email: "js@mail.com"},
    {id: 4, name: "Maria Antonieta", age: 29, profession: "dev2", email: "ma@mail.com"}
    ];

const PostType = new GraphQLObjectType({ //GraphQLObjectType tipo genericco
    name: 'Post',
    description: 'Representa os dados de um post',
    fields: ()=> ({
        id: {type: GraphQLNonNull(GraphQLString)}, 
        title:{type: GraphQLString},
        content: {type: GraphQLString},
        publishDate: {type: GraphQLString}
    })
})
const AuthorType = new GraphQLObjectType({
    name: "Author",
    fields: () => ({
      id: { type: GraphQLInt },
      name: { type: GraphQLString },
      books: {
        type: new GraphQLList(BookType),
        resolve: (author) => books.filter((book) => book.authorId === author.id),
      },
    }),
  });
  const BookType = new GraphQLObjectType({
    name: "Book",
    fields: () => ({
      id: { type: GraphQLInt },
      title: { type: GraphQLString },
      authorId: { type: GraphQLInt },
      author: {
        type: AuthorType,
        resolve: (book) => authors.find((author) => author.id === book.authorId),
      },
    }),
  });
const posts = [
    {id: 1, title: "Olá Mundo", content: "Novo olá", publishDate: "23/03/2023"},
    {id: 2, title: "Olá Brasil", content: "Novo olá Brasil", publishDate: "23/03/2023"},
    {id: 3, title: "Olá Europa", content: "Novo olá Europa", publishDate: "23/03/2023"}
]; 
const RootQuery = new GraphQLObjectType({ //criar a query
    name: "RootQueryType",
    fields:{
        book: {
            type: BookType,
            description: "A single book",
            args: { id: { type: GraphQLInt } },
            resolve: async (parent, args) =>{
                try {
                    const response = await axios.get(`http://localhost:3000/books/${args.id}`);
                    return response.data;
                } catch (error) {
                    throw new Error(`Livro com ID ${args.id} não encontrado.`);
                }            
            }
          },
          books: {
            type: new GraphQLList(BookType),
            description: "List of all books",
            resolve: async () => {
                try {
                    const response = await axios.get(`http://localhost:3000/books`);
                    return response.data;
                } catch (error) {
                    throw new Error(`Não encontramos sua requisição.`);
                }
            }       
          },
          authors: {
            type: new GraphQLList(AuthorType),
            description: "List of all authors",
            resolve: async () => {
                try {
                    const response = await axios.get(`http://localhost:3000/authors`);
                    return response.data;
                } catch (error) {
                    throw new Error(`Não encontramos sua requisição.`);
                }
            }       
          },
          author: {
            type: AuthorType,
            description: "A single author",
            args: { id: { type: GraphQLInt } },
            resolve: async (parent, args) =>{
                try {
                    const response = await axios.get(`http://localhost:3000/authors/${args.id}`);
                    return response.data;
                } catch (error) {
                    throw new Error(`Autor com ID ${args.id} não encontrado.`);
                }   
            }   
          },
       user:{
            type: UserType,
            description: "The user",
            args:{
                id: {type: GraphQLString}
            }, //definir o que quer utilizar como parametro de argumento
            resolve(parent, args){
                let user = users.find(user => user.id == args.id)
                if (!user) {
                    throw new Error(`Usuario com ID ${args.id} não encontrado.`); 
                }
                return user;
            }
        },
        users:{
            type: new GraphQLList(UserType),
            description: "List of all users",
            resolve: (parent, args)=>{
                return users
            }
        },
        posts: {
            type: new GraphQLList(PostType),
            description: "List of all posts",
            resolve: () => posts,
        },
        post:{
            type: PostType,
            description: "The post",
            args:{
                id: {type: GraphQLString}
            }, 
            resolve(parent, args){
                let post = posts.find(post => post.id == args.id)
                if (!post) {
                    throw new Error(`Usuario com ID ${args.id} não encontrado.`); 
                }
                return post;
            }
        }
    }
})

 const Mutation = new GraphQLObjectType({
    name:'Mutation',
    fields:{
        updatePostTitle:{
            type: PostType,
            args:{
                id: { type: GraphQLInt },
                title:{type: GraphQLString}                
            },
            resolve(parent, args){
                const postUpdate = posts.find(post => post.id === args.id)
                if (!postUpdate) {
                    throw new Error(`Post com ID ${args.id} não encontrado.`); 
                }
                postUpdate.title = args.title;
                return postUpdate;
            } //executar o codigo passando os argumentos
        } ,
        updateUserEmail:{
            type: UserType,
            args:{
                id: {type:GraphQLInt},
                email: {type: GraphQLString}
            },
            resolve(parent, args){
                const userUpdate = users.find(user => user.id === args.id)
                if (!userUpdate) {
                    throw new Error(`Post com ID ${args.id} não encontrado.`); 
                }
                if(!isValidEmail(args.email)){
                    throw new Error(`Email em formato inválido`); 
                }
                userUpdate.email = args.email;
                return userUpdate;
            }
        } 
    }
}) 
function isValidEmail(email) {
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailRegex.test(email);
}
/* //criando diretiva
class EmailDirective extends GraphQLDirective {
    constructor() {
        super({
            name: 'email',
            locations: [DirectiveLocation.FIELD_DEFINITION],
            args: {},
        });
    }

    visitFieldDefinition(field) {
        const { resolve = defaultFieldResolver } = field;
        field.resolve = async function (source, args, context, info) {
            const value = await resolve.call(this, source, args, context, info);
            if (!isValidEmail(value)) {
                throw new Error(`O campo "${info.fieldName}" não é um e-mail válido.`);
            }
            return value;
        };
    }
}


const schema = makeExecutableSchema({
    typeDefs: `
        directive @email on FIELD_DEFINITION

        type User {
            id: String!
            name: String
            email: String @email
        }

        type Mutation {
            updateUserEmail(id: Int!, email: String!): User
        }
    `,
    resolvers: {
        Mutation:{
        name:'Mutation',
        fields: {
            updateUserEmail: (parent, args) => {
                const userUpdate = users.find(user => user.id === args.id);
                if (!userUpdate) {
                    throw new Error(`Usuário com ID ${args.id} não encontrado.`);
                }
                userUpdate.email = args.email;
                return userUpdate;
                },
            }
        }
    },
    schemaDirectives: {
        email: EmailDirective,
    },
}); */
module.exports = new GraphQLSchema({ //exportando como schema do graphql
    query: RootQuery,
    mutation: Mutation   
})