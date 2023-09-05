import { Button, Container } from "@/styles/layout";
import { styled } from "styled-components";

const Wrapper = styled.div`
  background-color: #fff;
  position: relative;
  overflow: hidden;

  .text {
    max-width: 950px;
    margin: 150px auto;

    display: flex;
    flex-direction: column;
    align-items: center;

    h2 {
      padding: 0 50px;
      margin: 0 auto;
      font-size: 60px;
      line-height: 1.2;
      text-align: center;
  
      span {
        color: #2AA467;
      }
    }
  
    p {
      margin: 60px 0 80px 0;
      max-width: 800px;
      text-align: center;
      font-size: 25px;
      font-weight: 500;
    }

    button {
      text-transform: uppercase;
    }
  }

  img {
    width: 100%;
    margin-bottom: -10px;

    &.mobile {
      display: none;
    }
  }

  @media (max-width: 1400px) {
    .text {
      max-width: 840px;
      margin: 150px auto;

      h2 {
        font-size: 50px;
      }
    
      p {
        font-size: 22px;
      }
    }
  }

  @media (max-width: 991px) {
    .text {
      max-width: 800px;
      margin: 120px auto;

      h2 {
        padding: 0;
        font-size: 46px;
      }
    
      p {
        margin: 60px 0;
        max-width: 700px;
        font-size: 20px;
      }
    }
  }

  @media (max-width: 768px) {
    .text {
      max-width: 600px;
      margin: 100px 0;
      
      h2 {
        text-align: left;
        font-size: 40px;
      }
      
      p {
        text-align: left;
        margin: 60px 0;
        font-size: 20px;
      }
    }

    img {
      margin: 0 -40px;
      width: calc(100% + 80px);

      &.desktop {
        display: none;
      }
      &.mobile {
        display: flex;
      }
    }
  }

  @media (max-width: 620px) {
    .text {
      margin: 50px auto;

      h2 {
        font-size: 32px;
      }
    
      p {
        margin: 40px 0;
        font-size: 17px;
      }
    }
  }

  @media (max-width: 380px) {
    .text {
      h2 {
        font-size: 28px;
        max-width: 260px;
        margin-left: 0;
      }
    
    }
  }
`;

export default function SectionThree({ goToAction }: any) {
  return <Wrapper>
    <Container>
      <div className="text">
        <h2>As melhores páginas, <span>criadas por especialistas.</span></h2>
        <p>Nossa equipe de Designers e Programadores projetam a modernidade e desempenho que a sua página precisa para proporcionar o melhor resultado em suas vendas ou anúncios.</p>
        <Button onClick={() => goToAction()}>Quero a minha página</Button>
      </div>
      <img className="desktop" src="./images/home-section-three.png" alt="Landing Page Saturnia" />
      <img className="mobile" src="./images/home-section-three-mobile.png" alt="Landing Page Saturnia" />
    </Container>
  </Wrapper>
}