import { NextResponse } from "next/server";
// import { cpf } from "cpf-cnpj-validator";
import { connectToDatabase } from "@/services/database";
import bcryptjs from "bcryptjs";

export async function POST(request: Request) {
    const body = await request.json();

    var { name, document, email, phone, password } = body;
    
    // First and Last Name
    name = name?.trim();
    if(!name) return NextResponse.json({ name: "Obrigatório" });
    if(!name.split(" ")[1]) return NextResponse.json({ name: "Informe seu nome completo" });

    // Document (CPF)
    document = document?.replace(/[^a-zA-Z0-9 ]/g, '');
    if(!document || document.length < 10) return NextResponse.json({ document: "Obrigatório" });
    // if(!cpf.isValid(document)) return NextResponse.json({ document: "CPF inválido" });

    // E-mail
    if(!email) return NextResponse.json({ email: "Obrigatório" });
    if(!email.includes("@") || !email.includes(".")) return NextResponse.json({ email: "E-mail inválido" });

    // Phone
    if(!phone) return NextResponse.json({ phone: "Obrigatório" });
    if(phone.length < 10) return NextResponse.json({ phone: "Telefone inválido" });

    // Password
    if(!password) return NextResponse.json({ password: "Obrigatório" });
    if(password.length < 4) return NextResponse.json({ password: "Senha fraca" });

    // Connect to DB
    const { db } = await connectToDatabase();
    const usersCollection = db.collection("users");

    // Verify
    const existUser = await usersCollection.findOne({ document });
    if(existUser) return NextResponse.json({ error: "Você já tem uma conta" });

    const hash = await bcryptjs.hash(password, 12);

    // Insert
    // const { insertedId } = await usersCollection.insertOne({ name, document, email, phone, hash });
    const insertedId = "";

    return NextResponse.json({ success: true, user: { _id: insertedId, name, document, email, phone } });
}