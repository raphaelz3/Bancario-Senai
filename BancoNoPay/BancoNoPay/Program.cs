interface IContaBancaria
{
    void Depositar(decimal valor);
    void Sacar(decimal valor);
    void MostrarSaldo();
}

class ContaBancaria : IContaBancaria
{
    private decimal saldo;
    private static int proximoNumero = 1;
    public int NumeroConta;
    public string Titular;

    public ContaBancaria(string titular)
    {
        Titular = titular;
        NumeroConta = proximoNumero++;
        saldo = 0m;
    }

    public virtual void Depositar(decimal valor)
    {
        saldo += valor;
        Console.WriteLine($"Deposito de R$ {valor:F2} realizado.\nSaldo atual: {saldo:F2}");
    }

    public virtual void Sacar(decimal valor)
    {
        if (valor >saldo)
            Console.WriteLine($"Saldo unsuficiente.");
        else
        {
            saldo -= valor;
            Console.WriteLine($"Saque de R$ {valor} realizado. \nSaldo atual R$ {saldo:F2}");
        }
    }

    public void MostrarSaldo()
    {
        Console.WriteLine($"Conta: {NumeroConta} | Titular: {Titular} | Saldo: R${saldo:F2}");
    }
}

class ContaPoupanca : ContaBancaria
{
    public ContaPoupanca(string titular) : base(titular) { }

    public override void Depositar(decimal valor)
    {
        decimal bonus = valor * 0.01m;
        base.Depositar(valor +  bonus);
        Console.WriteLine($"Bonus de R$ {bonus:F2} adicionado!");
    }
}

class ContaCorrente : ContaBancaria
{
    public ContaCorrente(string titular) : base(titular) { }
    public override void Sacar(decimal valor)
    {
        decimal taxa = 2.50m;
        if (valor + taxa > 0)
        {
            base.Sacar(valor + taxa);
            Console.WriteLine($"Taxa de saque R$ {taxa} aplicada");
        }
    } 
}

class Banco
{
    private List<ContaBancaria> contas = new List<ContaBancaria>();

    public void CriarConta()
    {
        Console.WriteLine("Digital o nome da titular: ");
        string titular = Console.ReadLine();

        Console.WriteLine($@"
Escolha o tipo de conta
1 - Corrente
2 - Poupanca");
        Console.Write("> ");
        int tipo = int.Parse(Console.ReadLine());

        ContaBancaria novaConta = tipo == 1 ? new ContaCorrente(titular) : new ContaPoupanca(titular);

        contas.Add(novaConta);
        Console.WriteLine($"Conta {novaConta.NumeroConta} criada com sucesso\n");
    }

    public void Depositar()
    {
        Console.WriteLine("Digite o numero da conta");
        Console.WriteLine("> ");
        int numeroContaDigitado = int.Parse(Console.ReadLine());

        ContaBancaria contaBuscada = contas.Find(conta => conta.NumeroConta == numeroContaDigitado);

        if (contaBuscada != null)
        {
            Console.WriteLine("Digite o valor do deposito");
            Console.Write("> ");
            decimal valor = decimal.Parse(Console.ReadLine());
            contaBuscada.Depositar(valor);
        }
        else
        {
            Console.WriteLine("Conta nao encontrda!");
        }
    }

    class Program
    {
        static void Main()
        {
            Banco banco = new Banco();
            int opcao;

            do
            {
                Console.WriteLine($@"
===== Sistema Bancario =====
1 - Criar Conta
2 - Depositar
3 - Sacar
4 - Listar Contas
0 - Sair
============================");
                Console.Write("> ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        banco.CriarConta();
                        break;
                    case 2:
                        banco.Depositar();
                        break;
                    default:
                        Console.WriteLine("Opcao individual");
                        break;
                }
            } while (opcao != 0);
        }
    }
}