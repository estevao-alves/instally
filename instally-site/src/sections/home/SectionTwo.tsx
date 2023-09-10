import ActionButton from "@/components/actionButton"
import { Container } from "@/styles/layout"
import { styled } from "styled-components"

import CollectionsSvg from "@/sections/assets/collections.svg"

const Wrapper = styled.div`
  background-color: var(--white);
  padding: 120px 0;
  
  .content {
    display: flex;
    align-items: center;
    
    .text {
      max-width: 550px;
      gap: 40px;

      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: baseline;

      h2 {
        color: var(--purple-violet);
        font-size: 70px;
        font-weight: 800;
        
        span {
          color: var(--dark-gray);
        }
      }

      p {
        color: var(--dark-gray);
        font-size: 20px;
      }
    }
  }

  svg.collection {
    margin-right: -190px;
  }

  
  @media (max-width: 1600px) {
    svg.collection {
      margin-right: -100px;
    }
  }

  @media (max-width: 1400px) {
    svg.collection {
      margin-right: -50px;
    }
  }

  @media (max-width: 1200px) {
  }
  
  @media (max-width: 991px) {
    .content {
      margin: 0 auto;
      gap: 40px;

      justify-content: center;
      flex-direction: column;

      h2, p {
        text-align: center;
      }

      button.cta {
        width: 100%;
      }
    }
  }

  @media (max-width: 768px) {

  }

  @media (max-width: 576px) {

  }

  @media (max-width: 480px) {

  }
`;

export default function SectionOne() {
  return <Wrapper>
    <Container>
      <div className="content">
        <div className="text">
          <h2>Crie suas <span>coleções.</span></h2>
          <p>Instale e organize seus app em categorias e tenha mais tempo para o que realmente importa.</p>
          <ActionButton />
        </div>
        <CollectionsSvg className="collection" />
      </div>
    </Container>
  </Wrapper>
};