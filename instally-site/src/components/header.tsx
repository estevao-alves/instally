import { Container } from "@/styles/layout";
import styled from "styled-components";

import LogoSvg from "public/logo.svg";

const Wrapper = styled.div`
  padding: 10px;
  height: 100px;
  background-color: var(--dark-gray);

  display: flex;
  align-items: center;
  
  .content {
    display: flex;
  }

  .logo {
    width: 160px;
  }

  /*
  .options {
    display: flex;
    margin-left: auto;
    align-items: center;

    gap: 60px;

    span {
      font-size: 20px;
    }
  }
  */
`;

export default function Header() {
  return <Wrapper>
    <Container>
      <div className="content">
        <LogoSvg />
        {/* <div className="options">
          <span>Baixar</span>
          <span>Sobre</span>
          <span>Contato</span>
        </div>
        */}
        </div>
    </Container>
  </Wrapper>
};