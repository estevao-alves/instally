import { styled } from "styled-components";

const Option = styled.div`
  background: rgb(229 229 229);
  border-radius: 14px;
  margin: 0 0 10px;
  padding: 15px;
  border: 3px solid transparent;

  display: flex;
  justify-content: space-between;
  align-items: center;

  cursor: pointer;
  transition: 200ms ease-in-out;
  
  &:hover, &.active {
    border-color: var(--green);
    background: #d9efe4;
    color: #062d19;

    .check {
      background-color: var(--green);
    }
  }

  &.active {
    .check {
      background-image: url("data:image/svg+xml,%3Csvg width='28' height='24' viewBox='0 0 28 24' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M2 12.5L10 20L26 2' stroke='white' stroke-width='5'/%3E%3C/svg%3E%0A");
      background-repeat: no-repeat;
      background-position: center;
      background-size: 50%;
    }
  }
  
  .check {
    background: rgba(0,0,0,.1);

    --size: 30px;
    width: var(--size);
    min-width: var(--size);
    height: var(--size);
    border-radius: var(--size);
    margin: 0 20px 0 0;
  }

  .data {
    width: 100%;

    h5 {
      font-size: 20px;
    }

    p {
      font-size: 16px;
      line-height: 1.3;
      max-width: 90%;
    }
  }
`;

type InputTypes = {
  onClick: () => void;
  active: boolean;
  title: string;
}

export default function CheckBox({ onClick, active, title }: InputTypes) {
  return <Option className={active ? "active" : ""} onClick={onClick}>
    <div className="check"></div>
    <div className="data">
      <h5>{title}</h5>
    </div>
  </Option>
}