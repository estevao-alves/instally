import { Button, Container } from "@/styles/layout";
import { styled } from "styled-components";
import GoogleSVG from "./assets/google.svg"

const Wrapper = styled.div`
  padding: 100px 0;
  background-color: #F1F1EF;
  position: relative;

  > div {
    display: flex;
    align-items: center;
    flex-direction: column;
  }

  h2 {
    width: 100%;
    max-width: 850px;
    font-size: 40px;
    text-align: center; 
  }

  .google {
    width: 100%;
    margin: 100px auto;

    svg {
      max-height: 200px;
      width: 100%;
    }
  }

  @media (max-width: 1400px) {
    h2 {
      font-size: 36px;
    }

    .google {
      margin: 60px auto;
    }
  }

  @media (max-width: 991px) {
    h2 {
      font-size: 30px;
      max-width: 680px;
    }
  }

  @media (max-width: 576px) {
    h2 {
      font-size: 26px;
      max-width: 400px;
    }
  }

  @media (max-width: 480px) {
    padding: 60px 0;
    
    h2 {
      font-size: 22px;
      max-width: 360px;
    }

    .google {
      margin: 30px auto;
    }
  }

  @media (max-width: 380px) {
    h2 {
      font-size: 18px;
      max-width: 280px;
    }
  }

`;


export default function SectionFour({ goToAction }: any) {
  return <Wrapper>
    <Container>
      <h2>Criamos páginas que alcançam mais pesquisas e maior relevância no Google.</h2>
      <div className="google">
        <GoogleSVG />
      </div>
      <Button onClick={() => goToAction()}>Quero minha página</Button>
    </Container>
  </Wrapper>
}