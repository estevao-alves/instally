import { useEffect, useState } from "react";
import { styled } from "styled-components";

import { Button } from "@/styles/layout";
import Input from "@/components/inputs/official";
import { api } from "@/services/api";
import { formatPhoneNumber } from "@/helpers/format";
import InputSimple from "@/components/inputs/simple";
import InputPhone from "@/components/inputs/telephone";
import { useRouter } from "next/navigation";

const Wrapper = styled.div`
  width: 100%;
  height: 100%;
  background: #F2F2F2AA;
  backdrop-filter: blur(10px);

  position: absolute;
  top: 0;
  left: 0;
  padding: 20px 0 100px;
`;

const FormBox = styled.form`
  padding: 50px 40px 40px;
  border-radius: 14px;
  background: #FFF;
  max-width: 600px;

  position: sticky;
  top: 5vh;
  margin: 0 auto;

  h4 {
    text-align: center;
    font-size: 30px;
    color: var(--green);
    margin-bottom: 60px;
  }

  Button {
    width: 100%;
    margin: 30px 0 0;
  }

  @media (max-width: 620px) {
    margin: 10px;
    max-width: 100%;

    padding: 50px 20px 30px;

    h4 {
      font-size: 26px;
      margin-bottom: 40px;
    }
  }

  @media (max-width: 480px) {
    h4 {
      font-size: 28px;
      line-height: 1.2;
      max-width: 200px;
      margin: 0 auto 40px;
    }
  }

`;

export default function FormPricing() {

  const [visible, setVisible] = useState(true);
  const [form, setForm] = useState({}) as any;
  const [errors, setErrors] = useState(null) as any;
  const [loading, setLoading] = useState(false);

  const router = useRouter();

  async function handleSubmit(ev: any) {
    ev.preventDefault();
    
    if(loading) return;
    setLoading(true);

    // Enviar novo lead
    await api.post("/leads", form).then((response: any) => {
      setLoading(false);
      if(!response.data.success) return setErrors(response.data);

      // success
      setVisible(false);
      router.push("/#pricing")
    });
  }

  useEffect(() => setErrors(null), [form])
  
  if(!visible) return <></>;

  return (
    <Wrapper>
      <FormBox onSubmit={handleSubmit}>
        <h4>Descubra o investimento</h4>

        <label>Nome e sobrenome</label>
        <InputSimple placeholder="Nome e sobrenome" onChange={e => setForm({ ...form, name: e.target.value })} value={form?.name} error={errors?.name} />

        <label>E-mail</label>
        <InputSimple type="email" placeholder="seuemail@exemplo.com" onChange={e => setForm({ ...form, email: e.target.value })} value={form?.email} error={errors?.email} />

        <label>WhatsApp</label>
        <InputPhone onChange={(phone: string) => setForm({ ...form, phone })} value={form?.phone} error={errors?.phone} />

        <Button loading={loading ? 1 : 0}>Continuar</Button>
      </FormBox>
    </Wrapper>
  )

}