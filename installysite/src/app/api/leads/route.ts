import { NextResponse } from "next/server";

import { connectToDatabase } from "@/services/database";
import { sendMail } from "@/services/email/send";
import { formatOnlyNumbers } from "@/helpers/format";
import configs from "../../../../configs";
import { Auth } from "@/services/auth";
import { ObjectId } from "mongodb";

export async function POST(request: Request) {

  const body = await request.json();
  var { name, email, phone } = body;

  // Validations

  if(!name || name?.length < 1) return NextResponse.json({ name: "Preencha este campo" });

  if(!email || email?.length < 1) return NextResponse.json({ email: "Preencha este campo" });
  if(!email?.split("@") || email?.split("@")[1]?.length < 1 || !email?.split("@")[1]?.includes(".")) return NextResponse.json({ email: "E-mail inválido" });
  
  if(!phone) return NextResponse.json({ phone: "Preencha este campo" });
  if(phone?.length < 10) return NextResponse.json({ phone: "Número inválido" });

  // Format Data
  email = email?.toLowerCase();
  name = name?.split(" ")?.map((namePart: string) => `${namePart[0]?.toUpperCase()}${namePart?.slice(1)?.toLowerCase()}`).join(" ");
  const firstName = name?.split(" ")[0];
  
  // Insert new lead
  const { db } = await connectToDatabase();
  const leadsCollection = db.collection("leads");
  await leadsCollection.insertOne({ name, email, phone, createdAt: new Date() });

  await sendMail({
    to: [
      { name, email },
    ],
    subject: `Recebemos seu contato!`,
    message: `
      <span style="font-weight:bold;">Boas-vindas, ${firstName[0]?.toUpperCase() + firstName?.slice(1)?.toLowerCase()}!</span><br/><br/>

      Nossos especialistas já receberam suas informações e deverão entrar em contato com você o mais rápido possível.<br/><br/>

      Se você não quer esperar, é só clicar no botão abaixo para falar com a gente agora mesmo 😊.
    `,
    button: {
      text: "Chamar no WhatsApp",
      url: configs.whatsApp_CTA_Link + '&text=Olá, gostaria de criar minha página na internet! %0D%0DClique no botão ao lado para enviar &#707;&#707;&#707;'
    }
  }).then(async() =>
    await sendMail({
      to: [
        { name: "Gabriel Afonso", email: "gabrielafonsome@gmail.com" },
        { name: "Vitor Hugo", email: "vitorhugoh4@gmail.com" },
        { name: "Estevao Alves", email: "estevaoalvescg@gmail.com" },
      ],
      subject: `[+1] Novo lead na página`,
      message: `
        Um novo lead preencheu o formulário da página.<br/><br/>

        <span style="font-weight:bold;">Nome:</span> ${name}<br/>
        <span style="font-weight:bold;">E-mail:</span> ${email}<br/>
        <span style="font-weight:bold;">WhatsApp:</span> ${phone}<br/>
      `,
      button: {
        text: "Chamar no WhatsApp",
        url: `https://api.whatsapp.com/send?phone=${formatOnlyNumbers(phone)}`
      }
    })
  );

  return NextResponse.json({ success: true })
}

export async function GET(request: Request) {

  // Verify Token
  const auth = new Auth(request);
  if(!auth.isValid()) return NextResponse.json({ error: "Você não tem acesso!" });

  const { db } = await connectToDatabase();
  const leadsCollection = db.collection("leads");

  const leadsList: any[] | any[] = await leadsCollection.find({})?.sort({ createdAt: -1 }).toArray();

  return NextResponse.json(leadsList);
}