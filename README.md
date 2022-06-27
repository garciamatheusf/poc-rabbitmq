# poc-rabbitmq
POC para testar o rabbitmq.

Você já percebeu que na maioria dos hospitais e serviços públicos você recebe um ticket para atendimento, mas frequentemente um atendente chama um ticket e na sequência outro atendente chama mais outro, o que dificulta que as pessoas percebam que seu número foi chamado por causa da troca muito rápida no monitor. Nessa POC implementei uma maneira de tratar esse problema.

Nesse projeto implementei uma api pub de tickets sequenciais para atendimento e uma console application sub que fica pelo menos 3s exibindo o ticket recebido.

Para testar, basta subir o container do rabbitmq, criar a exchange TICKET_BROKER e a queue Q1.