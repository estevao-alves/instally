import { NextResponse } from "next/server";
import { sendMail } from "@/services/email/send";

export async function POST(request: Request) {

  const body = await request.json();
  const { name, email, template } = body;

  if(!name || name?.length < 1) return NextResponse.json({ error: "Obrigatório informar o nome do cliente!" });

  if(!email || email?.length < 1) return NextResponse.json({ error: "Obrigatório informar o email do cliente!" });
  if(!email?.split("@") || email?.split("@")[1]?.length < 1 || !email?.split("@")[1]?.includes(".")) return NextResponse.json({ error: "E-mail inválido" });

  await sendMail({
    to: [ { name, email } ],
    ...template
  });

  return NextResponse.json({ success: true })
}