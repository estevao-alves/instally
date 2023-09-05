import { NextResponse } from "next/server";
// import cpf from "cpf-cnpj-validator";
import { connectToDatabase } from "@/services/database";
import bcryptjs from "bcryptjs";
import { UserTypes } from "@/contexts/Dashboard";

export async function POST(request: Request) {
    const body = await request.json();

    var { document, password } = body;

    // Document (CPF)
    document = document?.replace(/[^a-zA-Z0-9 ]/g, '');
    if(!document || document.length < 10) return NextResponse.json({ document: "Obrigatório" });
    // if(!cpf.isValid(document)) return NextResponse.json({ document: "CPF inválido" });

    // Password
    if(!password) return NextResponse.json({ password: "Obrigatório" });

    // Connect to DB
    const { db } = await connectToDatabase();
    const usersCollection = db.collection("users");

    // Verify
    const user = await usersCollection.findOne({ document }) as UserTypes | any;
    if(!user) return NextResponse.json({ document: "Nenhuma conta encontrada" });

    const passwordCheck = await bcryptjs.compare(password, user.hash);
    if(!passwordCheck) return NextResponse.json({ password: "Senha inválida" });

    return NextResponse.json({ success: true, user: { _id: user._id, name: user?.name, document, email: user?.email, phone: user?.phone } });
}