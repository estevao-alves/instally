import { styled } from "styled-components";

export const Container = styled.div`
  width: 100%;
  max-width: 1400px;
  margin: 0 auto;
  padding: 0 60px;

  @media (max-width: 768px) {
    padding: 0 40px;
  }

  @media (max-width: 480px) {
    padding: 0 30px;
  }
`;

export const Button = styled.button<{ loading?: any, disabled?: number | any }>`
  background: var(--green);
  padding: 20px 60px;
  font-size: 20px;
  font-weight: 700;
  text-transform: uppercase;
  color: white;
  border-radius: 100px;
  cursor: pointer;

  transition: 200ms ease-in-out;

  &:hover {
    background: ${({ loading }) => loading === 1 ? `#15cd71` : ""};
  }

  display: flex;
  align-items: center;
  justify-content: center;

  svg {
    --size: 24px;
    width: var(--size);
    max-width: var(--size);

    margin: 0 20px 0 0;
  }

  @media (max-width: 576px) {
    font-size: 18px;
    padding: 20px;
    width: 100%;
  }

  @media (max-width: 400px) {
    font-size: 16px;
  }

  ${({ loading }) => loading === 1 && `
    color: transparent;
    position: relative;
    opacity: .8;
    cursor: progress;
    transition: 200ms ease-in-out 200ms, color 0s;

    &::before {
      content: "";
      width: 24px;
      height: 24px;
      position: absolute;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      margin: auto;
      border-radius: 100%;
      background: transparent;
      border: 6px solid #FFF;
      border-right-color: transparent;
      animation: 1s AnimationLoading infinite;

      transition: 200ms ease-in-out 200ms;

      @keyframes AnimationLoading {
        0% {
          transform: rotate(0);
        }
        100% {
          transform: rotate(360deg);
        }
      }
    }
  `};

  ${({ disabled }) => disabled === 1 && `
    opacity: .6;
    cursor: not-allowed;
  `};

`;

export const CategoryTitle = styled.div`
  font-size: 22px;
  font-weight: 800;
  margin: 50px 0 20px;
  position: relative;

  display: flex;
  align-items: center;
  --text-divider-gap: 1.2rem;

  &::after {
    content: "";
    height: 1px;
    background-color: rgba(0,0,0,.1);
    flex-grow: 1;
    margin-top: 2px;
    margin-left: var(--text-divider-gap);
  }

  @media (max-width: 576px) {
    font-size: 20px;
	}
`;