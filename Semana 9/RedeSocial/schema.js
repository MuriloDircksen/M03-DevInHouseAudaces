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
const RootQuery = new GraphQLObjectType({ //criar a query
    name: "RootQueryType",
    fields:{
       user:{
            type: UserType,
            args:{
                id: {type: GraphQLString}
            }, //definir o que quer utilizar como parametro de argumento
            resolve(parent, args){
                return books.find(book => book.id == args.id)
            }
        },
        users:{
            type: new GraphQLList(UserType),
            resolve: (parent, args)=>{
                return users
            }
        }
    }
})

module.exports = new GraphQLSchema({ //exportando como schema do graphql
    query: RootQuery
   
})