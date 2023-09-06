import { styled } from "styled-components";

const Wrapper = styled.div`
  padding: 40px;

  display: flex;
  justify-content: center;
  align-items: center;

  div {
    --size: 10px;
    opacity: .8;

    width: var(--size);
    height: var(--size);
    border-radius: var(--size);
    background-color: var(--green);
    margin: 0 6px;
    animation: 1400ms AnimationCircles infinite;

    @keyframes AnimationCircles {
      0% {
        transform: scale(1);
      }
      50% {
        transform: scale(1.4);
      }
      100% {
        transform: scale(1);
      }
    }
    
    &:nth-of-type(2) {
      animation-delay: 200ms;
    }

    &:nth-of-type(3) {
      animation-delay: 400ms;
    }
  }

`;

export default function LoadingMoreItems() {
  return <Wrapper>
    <div></div>
    <div></div>
    <div></div>
  </Wrapper>
}