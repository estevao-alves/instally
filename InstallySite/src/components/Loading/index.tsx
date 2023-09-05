import { styled } from "styled-components";

import IconBranding from "@/assets/icon.svg";

const Wrapper = styled.div`
  background: #FFF;
  height: 100vh;
  width: 100%;
  overflow: hidden;
  z-index: 1002;
  position: fixed;

  display: flex;
  justify-content: center;
  align-items: center;

  > svg {
    --size: 60px;

    width: var(--size);
    height: var(--size);

    animation: 2s Animation infinite ease-in-out;

    @keyframes Animation {
      0% {
        transform: scale(1);
      }
      50% {
        transform: scale(1.2);
      }
      100% {
        transform: scale(1);
      }
    }
  }
`;

export default function Loading()
{
  return <Wrapper>
    <IconBranding />
  </Wrapper>
}