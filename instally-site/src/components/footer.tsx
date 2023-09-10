import styled from "styled-components"

import { Container } from "@/styles/layout"

import InstallyGrayLogoSvg from "/public/installyGrayLogo.svg"
import SaturniaLogoSvg from "/public/saturniaLogo.svg"
import BrasilIconSvg from "/public/saturniaBrazilFlag.svg"

const Wrapper = styled.div`
  padding: 60px 0;
  background: var(--dark-gray);
  position: relative;

  .businessAndLinks {
    gap: 18px;

    display: flex;
    flex-direction: column;

    .installyGrayLogo {
      width: 260px;
      height: 100px; 
    }

    h3 {
      font-size: 20px;
      font-weight: 500;
      margin: 0 0 10px;
    }

    .number {
      display: flex;
      gap: 6px;
      
      .brasilIcon {
        margin-top: 1px;
        height: 20px;
        width: 20px;
      }
    }
  }

  .copyright {
    padding: 50px 0 0 0;

    display: flex;
    justify-content: space-between;

    .author {
      display: flex;
      
      span {
        margin-right: 10px;
      }

      .saturniaLogo {
        svg { height: 20px }
        path { fill: #fff }
      }
    }
  }
`;

export default function Footer() {
  return <Wrapper>
    <Container>
      <div className="content">
          <div className="businessAndLinks">
            <div className="installyGrayLogo"><InstallyGrayLogoSvg /></div>
            <h3>Saturnia Tecnologia LTDA.</h3>
            <span>Avenida Paulista, 1636 - Sala 1504 <br/>Edif. Paulista Corporate, São Paulo, Brasil</span>
            <span>contato@saturniatecnologia.com.br</span>
            <div className="number">
              <div className="brasilIcon"><BrasilIconSvg /></div>
              <span>+55 (11) 5194-2000</span>
            </div>
          </div>
      </div>

      <div className="copyright">
        <span>Copyright © 2023 Saturnia. Todos os direitos reservados.</span>
        <div className="author">
          <span>Powered by</span>
          <div className="saturniaLogo"><SaturniaLogoSvg /></div>
        </div>
      </div>

    </Container>
  </Wrapper>
};