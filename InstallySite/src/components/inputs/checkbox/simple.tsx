import HTMLReactParser from "html-react-parser";
import styled from "styled-components";

const Wrapper = styled.div<{ align?: string }>`
  display: flex;
  flex-direction: ${props => props.align === "left" ? "row" : "row-reverse"};
  align-items: center;
  margin: 0 0 10px;
  cursor: pointer;

  label {
    font-size: 14px;
    font-weight: 500;
    opacity: .8;
    margin: 0 10px;
    cursor: pointer;
    text-align: ${props => props.align || 'left'};

    user-select: none;
    -webkit-user-select: none;
    -moz-user-select: none;
    -o-user-select: none;
  }

  &.min {
    label {
      font-size: 13px;
    }
  }

  a {
    color: var(--green);
    text-decoration: none;
    font-weight: 700;
  }
`;

export const Check = styled.div<{ color?: string, active: number, themeMode?: string }>`
  min-width: 30px;
  max-width: 30px;
  height: 30px;
  background: ${props => props.active ? (props.color || 'var(--green)') : 'transparent'};
  border: 2px solid;
  border-color: ${props => props.active ? 'rgba(255,255,255,.4)' : 'rgba(0,0,0,.2)'};
  border-radius: 8px;
  position: relative;

  transition: .1s ease-in-out;

  ${props => props.active && `
    &::before {
      content: "";
      width: 8px;
      height: 4px;
      background: white;

      position: absolute;
      top: calc(50% - 1px);
      left: calc(50% - 7px);
      border-radius: 10px;
      transform: rotate(45deg);
    }

    &::after {
      content: "";
      width: 12px;
      height: 4px;
      background: white;

      position: absolute;
      top: calc(50% - 2px);
      left: calc(50% - 4px);
      border-radius: 10px;
      transform: rotate(-45deg);
    }
  `};
`;

type InputTypes = {
  color?: string;
  align?: "left" | "right";
  text: any;
  active: boolean;
  cb: (active: boolean) => void;
  className?: string;
  style?: any;
}

export default function CheckBoxSimple({ color, align = "left", text, active, cb, className, style }: InputTypes) {
  return <Wrapper onClick={() => cb(!active)} align={align} className={className} style={style}>
    <Check color={color} active={active ? 1 : 0} />
    <label>{HTMLReactParser(text)}</label>
  </Wrapper>
}