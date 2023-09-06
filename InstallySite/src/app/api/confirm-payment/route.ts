import { sendMail } from "@/services/email/send";
import { NextResponse } from "next/server";
import configs from "../../../../configs";
import { formatMoney } from "@/helpers/format";

export async function POST(request: Request) {
  const { data } = await request.json();

  const { amount, charges } = data.object;
  const { billing_details, receipt_url } = charges.data[0];
  const { name, email } = billing_details;

  if(!name) return NextResponse.json({ error: { message: "Impossível identificar o cliente." } })
  if(!email) return NextResponse.json({ error: { message: "E-mail de destino inválido." } })
  if(!amount) return NextResponse.json({ error: { message: "Valor da cobrança inválido." } })

  const firstName = name?.split(" ")[0];

  await sendMail({
    to: [ { name, email } ],
    subject: `Seu link chegou!`,
    message: `
      <span style="font-weight:bold;">Olá, ${firstName[0]?.toUpperCase() + firstName?.slice(1)?.toLowerCase()}!</span><br/><br/>

      Recebemos seu pagamento de ${formatMoney(amount)}.<br/><br/>

      Agora você já pode acessar seu link exclusivo clicando no botão abaixo, onde deverá nos encaminhar as informações necessárias para a sua nova página 😊.
    `,
    button: {
      text: "Acessar meu link",
      url: `${configs.domain}/f/${email}`
    }
  }).then(async() =>
    await sendMail({
      to: [
        { name: "Gabriel Afonso", email: "gabrielafonsome@gmail.com" },
        { name: "Vitor Hugo", email: "vitorhugoh4@gmail.com" },
        { name: "Estevao Alves", email: "estevaoalvescg@gmail.com" },
      ],
      subject: `[+1] Novo pagamento recebido`,
      message: `
        Acabamos de receber um novo pagamento em <span style="font-weight:bold;">CriarPagina.com.</span><br/><br/>

        <span style="font-weight:bold;">Nome:</span> ${name}<br/>
        <span style="font-weight:bold;">E-mail:</span> ${email}<br/>
        <span style="font-weight:bold;">Valor:</span> ${formatMoney(amount)}<br/>
      `,
      button: {
        text: "Ver recibo",
        url: receipt_url
      }
    })
  );

  return NextResponse.json({ success: true });
}