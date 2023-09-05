import { styled } from "styled-components";

export const Wrapper = styled.div`
  min-height: 100vh;
  padding: 60px 0;
  overflow: hidden;
  background-color: var(--background-color);

  @media (max-width: 576px) {
    padding: 40px 0;
  }
`;

export const Content = styled.div`
  max-width: 1000px;
  margin: 0 auto;
  
  .logo {
    max-width: 300px;
    max-height: 40px;
    margin: 0 0 60px;
  }

  .line {
    margin: 30px 0;
    height: 1px;
    background: rgba(0,0,0,.15);
    width: 100%;
  }

  h1 {
    font-size: 40px;
  }

  h2 {
    font-size: 20px;
    color: var(--green);
    margin: 50px 0 20px;
  }

  p {
    margin: 20px 0 0;

    span {
      font-weight: 700;

      &.green {
        color: var(--green);
      }
    }
  }
  
  @media (max-width: 991px) {
    .logo {
      max-width: 200px;
      margin: 0 0 40px;
    }

    h1 {
      font-size: 20px;
    }

    h2 {
      font-size: 16px;
      margin: 30px 0 30px;
    }
  }

  @media (max-width: 620px) {
    .logo {
      margin-bottom: 40px;
    }

    h1 {
      font-size: 26px;
    }
  }

`;

export const UploadItems = styled.div`
  
  .item-image {
    display: flex;
    align-items: center;

    background: rgba(0,0,0,.1);
    padding: 5px;
    border-radius: 10px;
    margin: 10px 0 0;

    --size: 50px;

    img {
      width: var(--size);
      min-width: var(--size);
      height: var(--size);
      border-radius: 6px;
      object-fit: cover;
    }

    span {
      width: 100%;
      padding: 0 20px;
      font-size: 14px;
      text-overflow: ellipsis;
      max-width: 100%;
      overflow: hidden;
    }

    svg.remove {
      max-width: 20px;
      width: 30px;
      width: var(--size);
      min-width: var(--size);
      height: var(--size);
      padding: 15px;
      border-radius: var(--size);;
      background: transparent;
      transition: 200ms ease-in-out;
      cursor: pointer;

      &:hover {
        background: rgba(0,0,0,.05);
      }
    }
  }
`;