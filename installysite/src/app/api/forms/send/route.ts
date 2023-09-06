import { NextResponse } from 'next/server';
import { ClientFormData } from '@/sections/forms';
import { formValidator } from '@/helpers/forms/validator-fields';
import { connectToDatabase } from '@/services/database';
import { sendMail } from '@/services/email/send';
import configs from '../../../../../configs';

export async function POST(request: Request) {
  
  var body = await request.json() as ClientFormData | any;

  try {
    const formErrors = formValidator(body);
    if(Object.keys(formErrors)?.length > 0) throw formErrors;

    const { db } = await connectToDatabase();
    const formCollection = db.collection("forms");

    body.createdAt = new Date();

    const { insertedId } = await formCollection.insertOne(body);
    if(!insertedId) throw "Não foi possível inserir as informações!";

    await sendMail({
      to: [
        { name: "Gabriel Afonso", email: "gabrielafonsome@gmail.com" },
        { name: "Vitor Hugo", email: "vitorhugoh4@gmail.com" },
        { name: "Estevao Alves", email: "estevaoalvescg@gmail.com" },
      ],
      subject: `[+1] Novo pagamento recebido`,
      message: `
        Novo formulário de informações preenchido por um cliente!

        <span style="font-weight:bold;">E-mail do cliente:</span> ${body.email}<br/>
      `,
      button: {
        text: "Acessar formulários",
        url: configs.domain + "/admin/forms"
      }
    })

    return NextResponse.json({ success: true, insertedId });
  
  } catch(error) {
    return NextResponse.json({ error });
  }
  
}