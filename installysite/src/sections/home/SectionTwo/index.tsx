import { Button, Container } from "@/styles/layout";
import { styled } from "styled-components";
import CheckSVG from "./assets/check.svg";

const Wrapper = styled.div`
  min-height: 100vh;
  padding: 80px 0 0;
  background-color: #f2f2f2;

  .wireframes {
    position: relative;

    img {
      width: 100%;
      max-width: 1920px;
      margin: 60px auto 0;
      display: flex;
    }

    &:after {
      content: "";
      background: linear-gradient(to bottom, #f2f2f2, transparent);
      width: 100%;
      height: 400px;

      position: absolute;
      top: 0;
      left: 0;
    }
  }

  @media (max-width: 576px) {
    padding: 40px 0 0;

    .wireframes {
      margin-top: -40px;
      z-index: 0;

      &:after {
        height: 200px;
      }
    }
  }
`;

const Content = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;

  .title {
    display: flex;
    flex-direction: column;
    align-items: center;

    span {
      text-transform: uppercase;
      font-weight: 700;
      color: #666;
    }

    h2 {
      max-width: 900px;
      font-size: 40px;
      text-align: center;
    }
  }

  .benefits {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 15px 80px;

    max-width: 800px;
    margin: 80px auto;

    .item {
      display: flex;
      align-items: center;

      svg {
        --size: 30px;
        height: var(--size);
        width: var(--size);
        min-width: var(--size);
      }

      span {
        font-size: 22px;
        font-weight: 700;
        margin: 0 0 0 20px;
      }
      
      &:nth-of-type(7),
      &:nth-of-type(8) {
        max-width: 300px;
      }
    }
  }

  @media (max-width: 1400px) {
    .title {
      h2 {
        font-size: 34px;
        max-width: 750px;
      }
    }

    .benefits {
      margin: 60px auto;
      gap: 15px 40px;

      .item {
        max-width: 500px!important;
        
        svg {
          --size: 24px;
        }
        span {
          font-size: 18px;
        }
      }
    }
  }

  @media (max-width: 991px) {
    .title {
      h2 {
        font-size: 30px;
        max-width: 650px;
      }
    }

    .benefits {
      grid-template-columns: 1fr;
    }
  }

  @media (max-width: 768px) {
    align-items: flex-start;

    .title {
      align-items: flex-start;

      h2 {
        text-align: left;
        max-width: 500px;
      }
    }

    .benefits {
      grid-template-columns: 1fr;
      margin-left: 0;
    }
  }

  @media (max-width: 576px) {
    .title {
      h2 {
        font-size: 20px;
      }
    }

    .benefits {
      margin: 40px 0;

      .item {
        svg {
          --size: 16px;

        }
        span {
          margin: 0 0 0 10px;
          font-size: 15px;
        }
      }
    }
  }

  @media (max-width: 400px) {
    .title {
      h2 {
        max-width: 300px;
      }
    }
  }
`;

export default function SectionTwo({ goToAction }: any) {
  return <Wrapper>
    <Container>
      <Content>
        <div className="title">
          <span>Landing Page</span>
          <h2>A página que você precisa para anunciar seu produto ou serviço na internet.</h2>
        </div>
        <div className="benefits">
          <div className="item">
            <CheckSVG />
            <span>Mais resultados</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Otimizada para o Google</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Mais relevância</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Design moderno</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Captura de clientes</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Navegação rápida</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Disponível em todas as telas</span>
          </div>
          <div className="item">
            <CheckSVG />
            <span>Personalizada para o seu produto</span>
          </div>
        </div>
        <Button onClick={() => goToAction()}>Quero a minha página</Button>
      </Content>
    </Container>

    <div className="wireframes">
      <img src="./images/home-section-two.png" alt="Landing Page Saturnia" />
    </div>
  </Wrapper>
}