# BigData-CodeChallenge
Conteúdo:
- Diretório: Gerador de Registros - pequeno script que eu fiz em C# (única plataforma disponível no escritório, mas posso portar pra Java sem problemas) para gerar os registros conforme exemplos dados no enunciado do desafio.
- Diretório: HttpLogs - código feito para o desafio, contendo:
    - BalancedInput: arquivo de input gerado pelo script C# com 100.000 registros dividos de maneira aleatória entre 100 usuários.
    - BalancedOutput: pasta contendo os arquivos de output para cada userId
    - JARs: pasta contendo os arquivos JAR do desafio: um compilado com a JRE 1.7 e outro com a JRE 1.8 para evitar problemas de compatibilidade
    - src: pasta com o código não compilado para avaliação
- Os prints da chamada e da resposta do Hadoop no ambiente Linux CentOS em que rodei os testes.
