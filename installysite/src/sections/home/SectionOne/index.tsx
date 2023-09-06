import { Button, Container } from "@/styles/layout";
import { styled } from "styled-components";

import VelocitySVG from "./assets/velocity.svg";
import GoogleSVG from "./assets/google-icon.svg";
import ResultsSVG from "./assets/results.svg";
import LogoSVG from "public/logo.svg";

const Wrapper = styled.div`
  min-height: 100vh;
  padding: 80px 0;
  overflow: hidden;

  display: flex;
  align-items: center;

  position: relative;

  .domain {
    margin-bottom: 100px;

    svg {
      height: 40px;
      max-height: 40px;
    }
  }
  
  .text {
    max-width: 660px;

    .text-footer {
      width: 100%;
    }

    h1 {
      font-size: 60px;
      font-weight: 800;
      line-height: 1.2;
    }

    p {
      font-size: 26px;
      font-weight: 500;
      margin: 40px 0;
    }
  }

  .benefits {
    width: max-content;
    margin-top: 150px;
    display: flex;

    .item {
      display: flex;
      align-items: center;
      margin-right: 40px;
      
      svg {
        --size: 40px;
        height: var(--size);
        width: var(--size);
        min-width: var(--size);
      }

      span {
        font-size: 18px;
        font-weight: 700;
        margin: 0 0 0 20px;
      }

      &:last-child {
        margin: 0;
      }
    }
  }

  .image {
    position: absolute;
    right: 100px;
    bottom: 0;

    img {
      width: 100%;
    }
  }

  @media (max-width: 1800px) {
    .text {
      max-width: 550px;

      h1 {
        font-size: 50px;
      }

      p {
        font-size: 24px;
      }
    }

    .benefits {
      margin-top: 100px;

      .item {
        svg {
          --size: 30px;
        }
        
        span {
          margin: 0 0 0 10px;
          font-size: 16px;
        }
      }
    }
  }

  @media (max-width: 1600px) {
    .text {
      max-width: 470px;

      h1 {
        font-size: 44px;
      }

      p {
        font-size: 20px;
      }
    }

    .image {
      max-width: 800px;
    }
  }

  @media (max-width: 1400px) {
    .domain {
      margin-bottom: 40px;
    }

    .image {
      right: -50px;
    }
  }

  @media (max-width: 1280px) {
    .image {
      max-width: 750px;
    }
  }

  @media (max-width: 1200px) {
    .text {
      max-width: 430px;

      h1 {
        font-size: 40px;
      }

      p {
        font-size: 18px;
      }
    }

    .image {
      right: -100px;
    }

    .benefits {
      .item {
        margin-right: 20px;
        
        svg {
          --size: 24px;
        }

        span {
          font-size: 16px;
        }
      }
    }
  }

  @media (max-width: 1100px) {
    .text {
      max-width: 400px;

      h1 {
        font-size: 36px;
      }
    }
    
    .image {
      max-width: 700px;
      right: -120px;
    }
  }

  @media (max-width: 991px) {
    padding: 40px 0;
    flex-direction: column;
    min-height: 1100px;

    .domain {
      display: flex;
      align-items: center;
      justify-content: center;
    }

    .text {
      display: flex;
      flex-direction: column;
      align-items: center;

      margin: 0 auto;
      text-align: center;

      Button {
        width: 100%;
      }
    }

    .benefits {
      margin-top: 40px;
      margin-left: auto;
      margin-right: auto;

      .item {
        span {
          font-size: 12px;
        }
      }
    }

    .image {
      right: -20px;
      left: -20px;
      margin: auto;

      max-width: 100vw;
      max-height: 550px;
    }
  }

  @media (max-width: 600px) {
    min-height: 1000px;

    .text {
      max-width: 500px;

      h1 {
        font-size: 32px;
      }
    }
  }

  @media (max-width: 500px) {
    min-height: 960px;
    align-items: flex-start;

    .domain {
      align-items: flex-start;
      justify-content: flex-start;
      margin: 0 0 30px;

      svg {
        height: 25px;
      }
    }

    .text {
      text-align: left;
      align-items: flex-start;
      max-width: 100%;

      h1 {
        max-width: 350px;
      }
      
      p {
        margin: 20px 0 30px;
      }

    }

    .benefits {
      .item {
        
        svg {
          --size: 20px;
        }

        span {
          font-size: 12px;
        }
      }
    }
  }

  @media (max-width: 520px) {
    min-height: 920px;
    align-items: flex-start;

    .text {
      text-align: left;
      align-items: flex-start;
      max-width: 100%;
      
      h1 {
        max-width: 300px;
        font-size: 27px;
      }
      
      p {
        margin: 20px 0;
        font-size: 16px;
      }
      
      .text-footer {
        display: flex;
        flex-direction: column-reverse;
      }

    }

    .benefits {
      justify-content: space-between;
      width: 100%;
      max-width: 280px;
      margin: 15px auto 25px;
      
      .item {
        margin: 0;
        flex-direction: column;
        
        svg {
          --size: 30px;
        }

        span {
          margin: 15px 0 0;
          font-size: 12px;
        }
      }
    }
  }

  @media (max-width: 480px) {
    min-height: 860px;
  }

  @media (max-width: 450px) {
    min-height: 840px;
  }

  @media (max-width: 420px) {
    min-height: 840px;
  }

  @media (max-width: 400px) {
    min-height: 820px;
  }

  @media (max-width: 380px) {
    min-height: 800px;
  }

`;

export default function SectionOne({ goToAction }: any) {
  return (
    <Wrapper>
      <Container>
        <div className="domain">
          <LogoSVG />
        </div>
        
        <div className="text">
          <h1>Chega de instalar apps um por um.</h1>
          <p>O instally veio para revolucionar a forma com que você gerencia seus aplicativos, permitindo que você instale vários apps de uma vez, apenas com um click.</p>
          
          <div className="text-footer">
            <Button onClick={() => goToAction()}>Baixar para windows</Button>

            <div className="benefits">
              <div className="item">
                <VelocitySVG />
                <span>Velocidade</span>
              </div>

              <div className="item">
                <GoogleSVG />
                <span>Alta conversão</span>
              </div>

              <div className="item">
                <ResultsSVG />
                <span>Resultados</span>
              </div>
            </div>
          </div>
        </div>

        <img className="image" src="./images/home-section-one.png" alt="Landing Page - Saturnia" />
      </Container>
    </Wrapper>
  )
}