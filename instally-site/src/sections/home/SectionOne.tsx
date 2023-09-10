import ActionButton from "@/components/actionButton"
import { Container } from "@/styles/layout"
import { styled } from "styled-components"

import BoyHoldingBoxesSvg from "@/sections/assets/boyHoldingBoxes.svg"
import BoyJumpingSvg from "@/sections/assets/boyJumping.svg"

const Wrapper = styled.div`
  background-color: var(--dark-gray);
  padding-top: 100px;
  padding-bottom: 180px;
  min-height: calc(90vh - 100px);
  
  display: flex;
  align-items: center;
  position: relative;
  overflow: hidden;

  .content {
    margin: 0 auto;
    max-width: 700px;
    gap: 40px;

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    h1 {
      font-size: 60px;
      font-weight: 800;

      text-align: center;

      span {
        color: var(--purple-simple);
        }
      }

    p {
      text-align: center;
    }
  }

  svg.elementSvg {    
    position: absolute;
    bottom: -10px;
    height: 100%;
    max-height: 38vw;
  }

  .leftImage {
    left: 0;
  }
  .rightImage {
    right: 0;
  }

  @media (max-width: 1400px) {
    min-height: 50vw;

    svg.elementSvg {
      max-height: 32vw;
    }
  }

  @media (max-width: 1200px) {
    min-height: 750px;
    padding-top: 100px;
    align-items: baseline;
  
    .content {
      max-width: 600px;
      h1 {
        font-size: 55px;
      }
    }

    svg.elementSvg {
      max-height: 35vw;
    }
  }

  @media (max-width: 991px) {
    min-height: 900px;

    svg.elementSvg {
      max-height: 46vw;
    }
  }
  
  @media (max-width: 768px) {
    min-height: 850px;
    padding-top: 60px;

    .content {
      max-width: 500px;

      h1 {
        font-size: 50px;
      }
      p {
        font-size: 14px;
      }
    }
    
    .leftImage {
      display: none;
    }

    svg.rightImage {
      width: 100%;
      margin: 0 auto;
      max-height: 400px;
    }
  }

  @media (max-width: 576px) {
    .content {
      max-width: 420px;
      h1 {
        font-size: 45px;
      }
    }
  }

  @media (max-width: 480px) {
    padding-top: 50px;
    min-height: 750px;

    .content {
      max-width: 380px;
      h1 {
        font-size: 36px;
      }
    }

    svg.rightImage {
      max-height: 320px;
    }
  }

  @media (max-width: 380px) {
    min-height: 720px;

    .content {
      max-width: 380px;
      h1 {
        font-size: 30px;
      }
    }
  }
`;

export default function SectionOne() {
  return <Wrapper>
    <Container>
      <div className="content">
        <h1>Chega de instalar <span>apps um por um.</span></h1>
        <p>O Instally veio para revolucionar a forma como você gerencia seus aplicativos, permitindo que você instale vários apps de uma vez, apenas com um clique.</p>
        <ActionButton />
      </div>

      <BoyJumpingSvg className="elementSvg leftImage" />
      <BoyHoldingBoxesSvg className="elementSvg rightImage" />
    </Container>
  </Wrapper>
};