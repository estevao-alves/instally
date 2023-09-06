import { useParams, useRouter } from "next/navigation";
import { useEffect, useState } from "react";

import configs from "../../../configs";

import { Wrapper, Content, Fields, UploadItems } from "./styles";
import { Button, Container } from "@/styles/layout";
import InputSelector from "@/components/inputs/Selector";
import InputSimple from "@/components/inputs/simple";
import CheckBox from "@/components/inputs/checkbox";
import InputUpload from "@/components/inputs/upload";
import TextAreaSimple from "@/components/inputs/textarea/simple";

import ColorPicker from "@/components/inputs/color/picker";
import InputPhone, { CountryPhoneTypes } from "@/components/inputs/telephone";
import LogoSVG from "public/logo.svg";
import TimesSvg from "@/assets/icons/times.svg";
import { api } from "@/services/api";
import { formValidator } from "@/helpers/forms/validator-fields";

export type ImageTypes = {
  file: File;
  url: string;
}

export type ClientFormData = {
  email: string,
  tipo?: string,
  titulo?: string,
  logo?: {
    inserir: boolean,
    itens?: ImageTypes[]
  },
  informacoesRelevantes?: string,
  imagens?: {
    inserir: boolean,
    itens?: ImageTypes[]
  },
  corDestaque?: string,
  dominio?: {
    possuiDominioRegistrado: boolean,
    url: string
  },
  video?: {
    inserir: boolean,
    url?: string
  },
  formularioInscricao?: {
    inserir: boolean,
  },
  avaliacoes?: {
    inserir: boolean,
    mensagem?: string
  },
  botaoFlutuanteWhatsApp?: {
    inserir: boolean,
    numero?: string
  },
  ctaDestino?: {
    destino: "Formulário" | "WhatsApp" | "Link específico",
    numero?: string,
    url?: string
  },
  aceitouOsTermos: boolean
}

const pageTypes = [
  "Página de vendas",
  "Página de serviço",
  "Página de captura de clientes",
  "Página de confirmação de inscrição",
  "Página de evento ou webinar",
  "Página de lançamento de produto",
  "Página de lançamento de curso"
];

export default function FormInfoSection() {

  const [form, setForm] = useState<ClientFormData | any>();
  const [errors, setErrors] = useState<ClientFormData | any>();

  const [loading, setLoading] = useState(false);

  const { email } = useParams() as any;
  const router = useRouter();

  useEffect(() => setForm({ email: email?.replace("%40", "@") }), [email]);

  const onChange = (data: any) => {

    if(!!errors && Object.keys(errors)?.length > 0 && errors[Object.keys(data)[0]] !== null) {
      const _errors = errors;
      _errors[Object.keys(data)[0]] = null;
      setErrors(_errors);
    }

    setForm({ ...form, ...data })
  };

  async function submit() {
    if(loading) return;

    try {
      setLoading(true);
  
      const formErrors = formValidator(form);
      if(Object.keys(formErrors)?.length > 0) throw formErrors;

      const { logo, imagens } = form;

      // Upload Images - Se houser imagens
      if(logo?.inserir || imagens?.inserir) {
        const formData = new FormData();
        formData.append("email", form?.email.replace(/[&\/\\#, +()$~%.@'":*?<>{}]/g, '-'));

        logo?.itens?.map(({ file }: ImageTypes, i: number) => {
          formData.append("logo[]", file);
        })

        imagens?.itens?.map(({ file }: ImageTypes, i: number) => {
          formData.append("imagens[]", file);
        })

        await api.post('/images/upload', formData, { headers: { 'Content-Type': 'multipart/form-data' } })
        .then((response: any) => {
          if(logo) logo.itens = response.data.logo;
          if(imagens) imagens.itens = response.data.images;
        })
        .catch((error: any) => ({ error: "Erro ao enviar o documento!" }));
      }

      // Enviar para a DB
      const { success, error } = await api.post("/forms/send", form).then(response => response.data);
      if(error) throw { error };
      
      if(success) router.push(configs.domain + "/forms/success")
            
    } catch(error: any) {
      if(error.error) return alert(error.error);

      setErrors(error);

      setTimeout(() => {
        const firstErrorClass = document.getElementsByClassName("error")[0];
        firstErrorClass?.scrollIntoView({ behavior: "smooth", block: "center" });
        setLoading(false);
      }, 1);
    }
  }

  return <Wrapper>
    <Container>
      <Content>
        <LogoSVG className="logo" />
      
        <div className="text">
          <h1>Você chegou na etapa mais importante!</h1>
          <h2>Encaminhe com atenção as informações necessárias para a construção da sua página através dos campos abaixo.</h2>
        </div>

        <div className="info">
          <div className="attention">*Atenção: essas informações são importantes para o processo de criação de sua página, caso envie informações incompletas, sensíveis ou que infrinjam as nossas políticas, elas não serão utilizadas.</div>
          <div className="timer">
            <span>Tempo médio para preenchimento</span>
            <span>10 minutos</span>
          </div>
        </div>

        <Fields>
          <div className={`item ${errors?.tipo ? "error" : ""}`}>
            <label>Qual tipo de página você gostaria?</label>
            <InputSelector options={pageTypes} cb={(opcaoSelecionada) => onChange({ tipo: opcaoSelecionada })} />
            { errors?.tipo ? <div className="errorMessage">{errors?.tipo}</div> : <></> }
          </div>

          <div className={`item ${errors?.titulo ? "error" : ""}`}>
            <label>Título da página:</label>
            <InputSimple placeholder="Nome da sua marca, produto ou lançamento" onChange={(e) => onChange({ titulo: e.target.value })} />
            { errors?.titulo ? <div className="errorMessage">{errors?.titulo}</div> : <></> }
          </div>

          <div className={`item ${errors?.logo ? "error" : ""}`}>
            <label>Gostaria de anexar uma logo?</label>

            <div className="double">
              <CheckBox active={form?.logo?.inserir} onClick={() => onChange({ logo: { inserir: true, itens: [] } })} title="Sim" />
              <CheckBox active={form?.logo && !form?.logo?.inserir} onClick={() => onChange({ logo: { inserir: false } })} title="Não" />
            </div>

            { errors?.logo ? <div className="errorMessage">{errors?.logo}</div> : <></> }

            { form?.logo?.inserir && <>
              { form?.logo?.itens?.length < 1 ? <InputUpload cb={(file, url) => onChange({ logo: { ...form?.logo, itens: [ { file, url } ] } })} /> :
                <UploadItems>
                  <div className="item-image">
                    <img src={form?.logo?.itens[0]?.url} />
                    <span>{form?.logo?.itens[0]?.file?.name}</span>
                    <TimesSvg className="remove" onClick={() => onChange({ logo: { inserir: true, itens: [] } })} />
                  </div>
                </UploadItems>
              }
            </> }
          </div>

          <div className={`item ${errors?.informacoesRelevantes ? "error" : ""}`}>
            <label>Descreva as informações mais relevantes que gostaria de adicionar em sua página:</label>
            <TextAreaSimple
              limit={3000}
              rows={5}
              placeholder="Ex: Meu produto é... ajuda as pessoas em... data do evento, lançamento, valores do curso.."
              onChange={(e) => onChange({ informacoesRelevantes: e.target.value })} />
            { errors?.informacoesRelevantes ? <div className="errorMessage">{errors?.informacoesRelevantes}</div> : <></> }
          </div>

          <div className={`item ${errors?.imagens ? "error" : ""}`}>
            <label>Você possui imagens para utilizarmos na sua página?</label>
            <div className="important">* Selecione até 5 imagens, caso não possua, selecionaremos opções dos nossos bancos de imagens.</div>
            
            <div className="double" style={{ margin: "20px 0" }}>
              <CheckBox active={form?.imagens?.inserir} onClick={() => onChange({ imagens: { inserir: true, itens: [] } })} title="Sim" />
              <CheckBox active={form?.imagens && !form?.imagens?.inserir} onClick={() => onChange({ imagens: { inserir: false } })} title="Não" />
            </div>

            { errors?.imagens ? <div className="errorMessage">{errors?.imagens}</div> : <></> }

            { form?.imagens?.inserir && <>
              { form?.imagens?.itens?.length < 5 ? <InputUpload cb={(file, url) => onChange({
                imagens: {
                  ...form?.imagens,
                  itens: [ ...(form?.imagens?.itens || []), { file, url } ]
                }
              }) } /> : <></> }

              <UploadItems>
                { form?.imagens?.itens?.map((item: { file: File, url: string }, i: number) => (
                  <div key={i} className="item-image">
                    <img src={item?.url} />
                    <span>{item?.file?.name}</span>
                    <TimesSvg className="remove" onClick={() => onChange({
                      imagens: {
                        ...form?.imagens,
                        itens: form?.imagens?.itens?.filter((_item: { file: File, url: string }, _i: number) => i !== _i)
                      }
                    })} />
                  </div>
                )) }
              </UploadItems>
            </> }
          </div>

          <div className={`item ${errors?.corDestaque ? "error" : ""}`}>
            <label>Escolha uma cor de destaque para usarmos em sua página:</label>
            <ColorPicker defaultColor="#FFF" cb={(newColor) => onChange({ corDestaque: newColor })} />
            { errors?.corDestaque ? <div className="errorMessage">{errors?.corDestaque}</div> : <></> }
          </div>

          <div className={`item ${errors?.dominio ? "error" : ""}`}>
            <label>Você já possui um endereço de domínio para esta página?</label>

            <div className="double">
              <CheckBox active={form?.dominio?.possuiDominioRegistrado} onClick={() => onChange({ dominio: { possuiDominioRegistrado: true } })} title="Sim" />
              <CheckBox active={form?.dominio && !form?.dominio?.possuiDominioRegistrado} onClick={() => onChange({ dominio: { possuiDominioRegistrado: false } })} title="Não" />
            </div>
            
            { form?.dominio &&
              <>
                { form?.dominio?.possuiDominioRegistrado ? <>
                  <label>Informe o domínio:</label>
                  <InputSimple placeholder="www.seudominio.com.br" onChange={(e) => onChange({ dominio: { ...form?.dominio, url: e.target.value } })} />
                </> : <>
                  <label>Escolha um domínio:</label>
                  <div className="important">* Se o domínio escolhido não estiver disponível para registro, entraremos em contato com você com opções disponíveis.</div>
                  <InputSimple placeholder="www.seudominio.com.br" onChange={(e) => onChange({ dominio: { ...form?.dominio, url: e.target.value } })} />
                </> }
              </>
            }

            { errors?.dominio ? <div className="errorMessage">{errors?.dominio}</div> : <></> }
          </div>

          <div className={`item ${errors?.video ? "error" : ""}`}>
            <label>Deseja inserir um vídeo na página?</label>

            <div className="double">
              <CheckBox active={form?.video?.inserir} onClick={() => onChange({ video: { inserir: true } })} title="Sim" />
              <CheckBox active={form?.video && !form?.video?.inserir} onClick={() => onChange({ video: { inserir: false } })} title="Não" />
            </div>
            
            { form?.video?.inserir && <>
              <label>Informe o link do vídeo no Youtube:</label>
              <InputSimple placeholder="https://youtube.com/" onChange={(e) => onChange({ video: { ...form?.video, url: e.target.value } })} />
            </> }

            { errors?.video ? <div className="errorMessage">{errors?.video}</div> : <></> }
          </div>

          <div className={`item ${errors?.formularioInscricao ? "error" : ""}`}>
            <label>Deseja adicionar um formulário de inscrição?</label>

            <div className="double">
              <CheckBox active={form?.formularioInscricao?.inserir} onClick={() => onChange({ formularioInscricao: { inserir: true } })} title="Sim" />
              <CheckBox active={form?.formularioInscricao && !form?.formularioInscricao?.inserir} onClick={() => onChange({ formularioInscricao: { inserir: false } })} title="Não" />
            </div>

            { errors?.formularioInscricao ? <div className="errorMessage">{errors?.formularioInscricao}</div> : <></> }
          </div>

          <div className={`item ${errors?.avaliacoes ? "error" : ""}`}>
            <label>Deseja adicionar avaliações em sua página?</label>

            <div className="double">
              <CheckBox active={form?.avaliacoes?.inserir} onClick={() => onChange({ avaliacoes: { inserir: true } })} title="Sim" />
              <CheckBox active={form?.avaliacoes && !form?.avaliacoes?.inserir} onClick={() => onChange({ avaliacoes: { inserir: false } })} title="Não" />
            </div>

            { form?.avaliacoes?.inserir && <>
              <TextAreaSimple
                limit={2000}
                rows={5}
                placeholder="Escreva até 5 avaliações"
                onChange={(e) => onChange({ avaliacoes: { ...form?.avaliacoes, mensagem: e.target.value } })} />
            </> }

            { errors?.avaliacoes ? <div className="errorMessage">{errors?.avaliacoes}</div> : <></> }
          </div>

          <div className={`item ${errors?.botaoFlutuanteWhatsApp ? "error" : ""}`}>
            <label>Deseja adicionar um botão flutuante do WhatsApp?</label>

            <div className="double">
              <CheckBox active={form?.botaoFlutuanteWhatsApp?.inserir} onClick={() => onChange({ botaoFlutuanteWhatsApp: { inserir: true } })} title="Sim" />
              <CheckBox active={form?.botaoFlutuanteWhatsApp && !form?.botaoFlutuanteWhatsApp?.inserir} onClick={() => onChange({ botaoFlutuanteWhatsApp: { inserir: false } })} title="Não" />
            </div>

            { form?.botaoFlutuanteWhatsApp?.inserir && <>
              <label>Número do WhatsApp:</label>
              <InputPhone onChange={(phone: string, country: CountryPhoneTypes) => onChange({ botaoFlutuanteWhatsApp: { ...form?.botaoFlutuanteWhatsApp, numero: phone } })} />
            </> }

            { errors?.botaoFlutuanteWhatsApp ? <div className="errorMessage">{errors?.botaoFlutuanteWhatsApp}</div> : <></> }
          </div>

          <div className={`item ${errors?.ctaDestino ? "error" : ""}`}>
            <label>Para onde os botões da página devem levar as pessoas?</label>

            <div style={{ margin: "10px 0" }}>
              <CheckBox active={form?.ctaDestino?.destino === "Formulário"} onClick={() => onChange({ ctaDestino: { destino: "Formulário" } })} title="Formulário" />
              <CheckBox active={form?.ctaDestino?.destino === "WhatsApp"} onClick={() => onChange({ ctaDestino: { destino: "WhatsApp" } })} title="WhatsApp" />
              <CheckBox active={form?.ctaDestino?.destino === "Link específico"} onClick={() => onChange({ ctaDestino: { destino: "Link específico" } })} title="Link específico" />
            </div>
            
            { form?.ctaDestino?.destino === "WhatsApp" && <>
              <label>Informe o número do WhatsApp:</label>
              <InputPhone onChange={(phone: string, country: CountryPhoneTypes) => onChange({ ctaDestino: { ...form?.ctaDestino, numero: phone } })} />
            </> }

            { form?.ctaDestino?.destino === "Link específico" && <>
              <label>Informe o link:</label>
              <InputSimple placeholder="https://" onChange={(e) => onChange({ ctaDestino: { ...form?.ctaDestino, url: e.target.value } })} />
            </> }

            { errors?.ctaDestino ? <div className="errorMessage">{errors?.ctaDestino}</div> : <></> }
          </div>

          <div className="item" style={{ margin: "80px 0" }}>
            <Button id="submitButton" onClick={() => submit()} loading={loading ? 1 : 0}>Enviar agora</Button>
          </div>

        </Fields>

      </Content>
    </Container>
  </Wrapper>
}