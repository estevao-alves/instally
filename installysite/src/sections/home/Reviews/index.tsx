import styled from "styled-components";

import GoogleReviewSvg from "./assets/google-review-info.svg";
import FiveStarsSvg from "./assets/five-stars.svg";

import { Button, Container } from "@/styles/layout";

const Wrapper = styled.div`
  background: #F2F2F2;
`;

const Content = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  color: #1a1a1aEE;

  position: relative;

  > svg {
    margin: 0 0 150px;
    max-width: 800px;
  }

  h2 {
    font-size: 40px;
    text-align: center;
    max-width: 680px;
    margin: 0 auto;

    span {
      font-weight: 500;
    }
  }

  h3 {
    max-width: 800px;
    font-size: 36px;
  }

  @media (max-width: 991px) {
    h2 {
      max-width: 500px;
      font-size: 30px;
    }

    h3 {
      max-width: 600px;
      font-size: 26px;
    }
  }

  @media (max-width: 480px) {
    h2 {
      max-width: 500px;
      font-size: 26px;
    }

    h3 {
      font-size: 30px;
      margin: 0 10px;
      text-align: left;
    }
  }
`;

const WrapperReviews = styled.div`
  position: relative;
`;

export const ReviewsList = styled.div`
  margin: 100px 0 60px;

  .double {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .review2 {
    max-width: 500px;
    margin: 0 0 -40px 60px;
  }

  .review3 {
    max-width: 500px;
    margin: 60px 60px 60px 0;
  }

  .review5 {
    max-width: 700px;
    margin: 0 auto;
  }

  @media (max-width: 991px) {
    .double {
      flex-direction: column;
    }

    > div, > div > div {
      margin-left: 0!important;
      margin-right: 0!important;
      margin-top: 0!important;
      max-width: 100%!important;
    }
  }

`;

export const Review = styled.div`
  width: 100%;
  padding: 40px;
  border-radius: 18px;
  box-shadow: 0 10px 20px rgba(0,0,0,.06);
  background-color: white;

  .head {
    display: flex;
    align-items: center;
    margin: 0 0 40px;

    --image-size: 50px;

    img {
      min-width: var(--image-size);
      max-width: var(--image-size);
      border-radius: var(--image-size);
      margin-right: 20px;
    }

    h5 {
      font-size: 16px;
      font-weight: 700;
      margin-bottom: -2px;
    }

    .stars {
      margin: 6px 0 -6px;
      max-width: 90px;
    }
  }

  p {
    margin: 16px 0 0;
  }

  .google {
    margin: 40px 0 0;
    max-width: 300px;
  }

  @media (max-width: 991px) {
    margin-bottom: 20px!important;
  }
`;

const ReviewFooter = styled.div`
  min-height: 600px;
  background-image: url("./images/home-reviews.png");
  background-repeat: no-repeat;
  background-size: contain;
  background-position: bottom;
  position: relative;

  &::before {
    content: "";
    width: 100%;
    height: 100%;
    background: linear-gradient(to bottom, #f2f2f2 20%, transparent);
    position: absolute;
    top: 0;
    left: 0;
    z-index: 1;
  }
  
  .content {
    max-width: 800px;
    padding: 160px 0 0;
    margin-left: auto;
    z-index: 2;
    position: relative;
    
    h2 {
      font-size: 36px;
      margin: 0 0 60px;
  
      span {
        font-weight: 300;
      }
    }
  }

  @media (max-width: 1600px) {
    min-height: 500px;

    .content {
      h2 {
        max-width: 600px;
        font-size: 28px;
      }
    }
  }

  @media (max-width: 1400px) {
    &::before {
      background: linear-gradient(to bottom, #f2f2f2 30%, transparent);
    }
    
    .content {
      max-width: 700px;
    }
  }

  @media (max-width: 1200px) {
    &::before {
      background: linear-gradient(to bottom, #f2f2f2 40%, transparent);
    }

    .content {
      padding: 80px 0;
      max-width: 600px;
    }
  }

  @media (max-width: 991px) {
    min-height: auto;

    .content {
      text-align: center;
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      margin: 0 auto;
      padding: 50px 0 160px;

      h2 {
        max-width: 460px;
        font-size: 22px;
        margin: 0 0 20px;
      }
    }
  }

  @media (max-width: 768px) {
    &::before {
      background: linear-gradient(to bottom, #f2f2f2 50%, transparent);
    }
  }

  @media (max-width: 576px) {
    &::before {
      background: linear-gradient(to bottom, #f2f2f2 60%, transparent);
    }

    .content {
      padding: 20px 0 160px;

      h2 {
        max-width: 460px;
        font-size: 20px;
        margin: 0 0 20px;
      }

      Button {
        max-width: max-content;
        padding-left: 40px;
        padding-right: 40px;
      }
    }
  }

  @media (max-width: 480px) {
    &::before {
      background: linear-gradient(to bottom, #f2f2f2 70%, transparent);
    }

    .content {
      padding: 20px 0 160px;

      h2 {
        max-width: 400px;
        font-size: 18px;
      }

      Button {
        max-width: max-content;
        padding-left: 40px;
        padding-right: 40px;
      }
    }
  }

`;


export default function Reviews({ goToAction }: any) {
  return <Wrapper>
  <Container>
    <Content>
      <h2>Eles acreditaram no nosso trabalho <span>e juntos construímos histórias de sucesso</span></h2>

      <WrapperReviews>
        <ReviewsList>
          <div className="double">
            <Review className="review1">
              <div className="head">
                <img src="/images/reviews/marcia.png" />
                <div className="data">
                  <h5>Marcia Botigelli</h5>
                  <FiveStarsSvg className="stars" />
                </div>
              </div>
              <p>Trabalhar com a Saturnia é se sentir diferenciado. Pra nós é muito importante o resultado que o nosso cliente vai ter através do nosso trabalho, e sentimos essa sintonia com a Saturnia em relação as nossas necessidades. Além é claro da pontualidade e dedicação. Confiança define bem nosso sentimento.</p>
              <GoogleReviewSvg className="google" />
            </Review>

            <Review className="review2">
              <div className="head">
                <img src="/images/reviews/joao.png" />
                <div className="data">
                  <h5>João Salles</h5>
                  <FiveStarsSvg className="stars" />
                </div>
              </div>
              <p>Simplesmente incrível!</p>
              <p>Sou cliente a 3 anos. E não 3 meses. Trabalho de tecnologia para meu e-commerce foi desenvolvido com muito profissionalismo e competência.</p>
              <p>Usaram o que há de melhor na tecnologia e atualizado em nível internacional.</p>
              <p>Indico 👏👏👏👏👏👏👏</p>
              <GoogleReviewSvg className="google" />
            </Review>
          </div>

          <div className="double">
            <Review className="review3">
              <div className="head">
                <img src="/images/reviews/mariah.png" />
                <div className="data">
                  <h5>Mariah Assouline</h5>
                  <FiveStarsSvg className="stars" />
                </div>
              </div>
              <p>Sou profissional na área da saúde e durante muito tempo procurei uma equipe que me ajudasse a chegar mais longe e ter resultados.</p>
              <p>Mas eu queria profissionais sérios e competentes. Na SATURNIA eu tive uma experiência incrível desde o primeiro contato, o que ainda acontece até hoje.</p>
              <p>Moro na França com uma diferença horária importante, e mesmo assim sempre me deram prioridade e me responderam com maestria. Nos tornamos uma boa parceria. Muito obrigada.</p>
              <GoogleReviewSvg className="google" />
            </Review>

            <Review className="review4">
              <div className="head">
                <img src="/images/reviews/guida.png" />
                <div className="data">
                  <h5>Guida Tolfo</h5>
                  <FiveStarsSvg className="stars" />
                </div>
              </div>
              <p>Excelente entrega!! Confiança e Credibilidade! Segurança e velocidade do site top! Equipe técnica sempre disposta a ajudar!!</p>
              <GoogleReviewSvg className="google" />
            </Review>
          </div>

          <Review className="review5">
            <div className="head">
              <img src="/images/reviews/gdo.png" />
              <div className="data">
                <h5>Grupo Dê Ouvidos</h5>
                <FiveStarsSvg className="stars" />
              </div>
            </div>
            <p>Trabalho com a Saturnia a alguns anos, agilidade, prestatividade e a entrega de um excelente serviço são primordiais.</p>
            <GoogleReviewSvg className="google" />
          </Review>
        </ReviewsList>
      </WrapperReviews>
    </Content>
  </Container>

  <ReviewFooter>
    <Container>
      <div className="content">
        <h2>Venha fazer parte dos +200 casos de sucesso conquistados nos últimos anos.</h2>
        <Button onClick={() => goToAction()}>Quero a minha página</Button>
      </div>
    </Container>
  </ReviewFooter>
</Wrapper>
}