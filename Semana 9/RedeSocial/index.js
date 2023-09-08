const express = require("express");
const { graphqlHTTP } = require("express-graphql");

const app = express();
const schema = require('./schema');


//schema

app.use('/graphql', graphqlHTTP({
    graphiql: true, //define a pagina padrão do graphql para testar igual o swagger
    schema: schema
}))
app.get('/', (req, res)=> res.send('GraphQL is running on /graphql'));

app.listen(3000, ()=> console.log('Server is running'));