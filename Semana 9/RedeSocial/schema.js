const { type } = require('express/lib/response')
const {GraphQLObjectType, GraphQLInt, GraphQLSchema, GraphQLString, GraphQLNonNull, GraphQLList} = require('graphql') // criando varios schemas

//tipagem para os user

const UserType = new GraphQLObjectType({ //GraphQLObjectType tipo genericco
    name: 'User',
    description: 'Representa os dados de um usuario',
    fields: ()=> ({
        id: {type: GraphQLNonNull(GraphQLString)}, 
        name:{type: GraphQLString},
        age: {type: GraphQLInt},
        profession: {type: GraphQLString}
    })
})
const users = [
    {id: 1, name: "Jose Andrade", age: 23, profession: "dev1"},
    {id: 2, name: "Neusa Maria", age: 24, profession: "dev1"},
    {id: 3, name: "Jose Saramago", age: 23, profession: "dev3"},
    {id: 4, name: "Maria Antonieta", age: 29, profession: "dev2"}
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

const posts = [
    {id: 1, title: "Olá Mundo", content: "Novo olá", publishDate: "23/03/2023"},
    {id: 2, title: "Olá Brasil", content: "Novo olá Brasil", publishDate: "23/03/2023"},
    {id: 3, title: "Olá Europa", content: "Novo olá Europa", publishDate: "23/03/2023"}
]; 
const RootQuery = new GraphQLObjectType({ //criar a query
    name: "RootQueryType",
    fields:{
       user:{
            type: UserType,
            description: "The user",
            args:{
                id: {type: GraphQLString}
            }, //definir o que quer utilizar como parametro de argumento
            resolve(parent, args){
                return users.find(user => user.id == args.id)
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
                return posts.find(post => post.id == args.id)
            }
        }
    }
})

module.exports = new GraphQLSchema({ //exportando como schema do graphql
    query: RootQuery
   
})