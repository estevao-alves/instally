import { Button, Container } from "@/styles/layout";
import { styled } from "styled-components";
import HTMLReactParser from "html-react-parser";

import Form from "./form";
import configs from "../../../../configs";

import PlanoAnualSVG from "./assets/plano-anual.svg";
import PlanoMensalSVG from "./assets/plano-mensal.svg";
import CheckIconSVG from "./assets/check.svg";
import WhatsAppIcon from "./assets/whatsapp.svg";

const Wrapper = styled.div`
  min-height: 100vh;
  padding: 80px 0;
  background-color: #f2f2f2;

  position: relative;

  h2 {
    font-size: 36px;
    margin: 0 auto 20px;
    text-align: center;
  }

  @media (max-width: 991px) {
    h2 {
      max-width: 500px;
      font-size: 30px;
    }
  }

  @media (max-width: 480px) {
    padding: 50px 0;

    h2 {
      font-size: 22px;
      margin: 0 auto;
    }
  }
`;

const Plans = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 30px;

  max-width: 1100px;
  margin: 60px auto;

  .item {
    background-color: #D1DED4;
    padding: 40px;
    border-radius: 14px;

    display: flex;
    flex-direction: column;

    .head {
      display: flex;
      align-items: center;
      margin: 0 0 40px;

      svg {
        --size: 55px;
        height: var(--size);
        width: var(--size);
        min-width: var(--size);
      }

      .name {
        text-transform: uppercase;
        margin-left: 20px;

        span {
          font-weight: 700;
          font-size: 20px;
          line-height: 1;
          opacity: .5;
        }

        h5 {
          font-size: 24px;
          line-height: 1;
        }
      }
    }
    
    .benefits {
      height: 100%;
      min-height: 240px;

      > div {
        margin: 0 0 10px;
        display: flex;
        align-items: center;
        font-size: 18px;
      }

      .free {
        margin: 0 0 0 10px;
        font-size: 12px;
        font-weight: 600;
        text-transform: uppercase;
        color: white;
        background: var(--green);

        display: flex;
        align-items: center;
        justify-content: center;
        padding: 4px 8px;
        border-radius: 6px;
      }
      
      svg {
        margin: 3px 13px 0 0;

        --size: 20px;
        height: var(--size);
        width: var(--size);
        min-width: var(--size);   
      }
    }

    .footer {
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      height: 100%;

      .price {
        span {
          font-size: 18px;
          font-weight: 700;
          opacity: .6;
        }

        h6 {
          font-size: 34px;
          line-height: 1.2;
        }

        .toSave {
          font-size: 14px;
          font-weight: 600;
          color: var(--green);
        }
      }

      p {
        font-size: 16px;
        margin: 20px 0 40px;
        opacity: .7;
      }

      Button {
        margin: 0 auto;
      }
    }
  }

  @media (max-width: 991px) {
    grid-template-columns: 1fr;
  }

  @media (max-width: 576px) {
    .item {
      padding: 30px;

      .head {
        margin: 0 0 30px;

        svg {
          --size: 40px;
          margin-top: 5px;
        }

        .name {
          margin-left: 15px;

          span {
            font-size: 14px;
          }

          h5 {
            font-size: 18px;
          }
        }
      }
    }
  }

  @media (max-width: 480px) {
    margin: 40px 0;
  }

  @media (max-width: 420px) {
    .item {
      padding: 30px;

      .head {
        svg {
          --size: 30px;
          margin-top: 10px;
        }

        .name {
          margin-left: 15px;

          span {
            font-size: 14px;
          }

          h5 {
            font-size: 14px;
          }
        }
      }

      .benefits {
        min-height: auto;
        margin-bottom: 20px;

        > div {
          margin: 0 0 10px;
          display: flex;
          font-size: 14px;
        }
        
        svg {
          margin: 3px 10px 0 0;

          --size: 18px;
        }
      }

      .footer {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        height: 100%;

        .price {
          span {
            font-size: 16px;
          }

          h6 {
            font-size: 30px;
          }
        }

        p {
          margin: 20px 0 30px;
        }
      }
    }
  }
`;

type PlanTypes = {
  icon?: any,
  title1: string,
  title2: string,
  benefits: string[],
  pricingMethod: string,
  toSaveMoney?: string,
  price: number,
  description: string,
  whatsAppMessage: string
}

export default function Pricing() {

  const plans: PlanTypes[] = [
    {
      icon: <PlanoAnualSVG />,
      title1: "Assinatura",
      title2: "Anual",
      benefits: [
        "Hospedagem <span className='free'>Incluso</span>",
        "Certificado de segurança",
        "Otimização para o Google"
      ],
      pricingMethod: "Pagamento Anual",
      price: 497,
      description: "Um plano acessível para quem busca economia em sua primeira página na internet.",
      whatsAppMessage: "Olá, quero a minha página na internet!"
    },
    {
      icon: <PlanoMensalSVG />,
      title1: "Assinatura",
      title2: "Mensal",
      benefits: [
        "Hospedagem <span className='free'>Incluso</span>",
        "Domínio .com.br <span className='free'>Incluso</span>",
        "Certificado de segurança",
        "Otimização para o Google",
        "Suporte do desenvolvedor para novas atualizações",
      ],
      pricingMethod: "Pagamento Mensal",
      toSaveMoney: "* Economize 44% em todos os meses.",
      price: 280,
      description: "Para quem busca por uma página na internet com suporte dos nossos desenvolvedores todos os meses.",
      whatsAppMessage: "Olá, quero a minha página na internet com suporte dos desenvolvedores!"
    }
  ]

  return <Wrapper id="pricing">
    <Container>
      <h2>Aproveite essa oferta</h2>
      
      <Plans>
        { plans.map((plan, i: number) => (
          <div className="item" key={i}>
            <div className="head">
              {plan.icon}
              <div className="name">
                <span>{plan.title1}</span>
                <h5>{plan.title2}</h5>
              </div>
            </div>
            <div className="benefits">
              {plan.benefits.map((benefit, iBenefity) => <div key={iBenefity}><CheckIconSVG /> {HTMLReactParser(benefit)}</div>)}
            </div>

            <div className="footer">
              <div className="price">
                <span>{plan.pricingMethod}</span>
                <h6>R$ {plan.price}</h6>
                { plan.toSaveMoney && <div className="toSave">{plan.toSaveMoney}</div> }
              </div>

              <p>{plan.description}</p>
            
              <Button onClick={() => window.open(configs.whatsApp_CTA_Link + `&text=${plan.whatsAppMessage} %0D%0DClique no botão ao lado para enviar >>>`)}><WhatsAppIcon /> Continuar</Button>
            </div>
          </div>
        )) }
      </Plans>
    </Container>

    <Form />
  </Wrapper>
  }