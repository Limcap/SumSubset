# API - Subconjunto de array de soma
Teste para a vaga de Backend Developer na Audaces.

### Informações
Desenvolvi a aplicação usando GraphQL e também REST.
A documentção das classes está feita usando o formato de tags XML para documentação no proprio código.
Existe dois endpoints tres endpoints principais:

- "/" ou "/help" : Exibe estas informações sobre a API
- "/rest" : prefixo da API REST
- "/graphql" : endpoint da api GraphQL

Ao ser executado o serviço fica disponível na porta 5000 http ou 5001 para https.
Para efeito de simplicidade desativei o uso de https nas configurações da aplicação porém ainda é possivel acessar pela porta 5001.
Percebi que em alguns computadores o Banana Cake Pop so funciona direito na porta 5001.
Para acessar o Banana Cake Pop no navegador digite o endereço http://localhost:5000/graphql ou https://localhost5001/graphql.

Para utilizar as APIs siga as orientações abaixo:

### API REST:
Os endpoints são:
```
/rest/previousRequest/id/{id} :  exibe uma request feita anteriormente especificando seu id.
/rest/previousRequests : exibe todas as requests gravadas.
/rest/previousRequests?initialDate=2022/05/31&finalDate=2022/06/01 : exibe todas as requests feitas entre as datas especificadas.
/rest/solveQuiz/{sequence}/{target} : Resolve um quiz de subconjunto de array de soma. o sequence deve ser numeros separados por virgula e target somente um numero.
/rest/solveQuiz?sequence=1,2,3,4,5,6,7&target=19 : Outra forma de acessar o endpoint acima.
```

### API GraphQL:
Faz exatamente as mesmas operações da API rest, utilizando a mesma nomecratura, porém na sintaxe devida do framework GraphQL.
Ao acessar o endpoint /graphql pelp navegador é possivel acessar a interface do Banana Cake Pop, facilitando a consulta da API

As queries disponiveis são:

```
query {
   previousRequest(id: 1) {
      id date target sequence solution
   }
   previousRequests(initialDate:"2022/05/31" finalDate:"2022/06/01" ) {
      id date target sequence solution
   }
   solveQuiz(sequence:[1,2,3,4,5,6] target:13) {
      hasSolution solution
   }
}
```