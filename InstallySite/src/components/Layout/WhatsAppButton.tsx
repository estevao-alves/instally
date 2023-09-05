import { styled } from "styled-components";

import WhatsAppIcon from "@/assets/icons/whatsapp.svg";

const Wrapper = styled.button`
	background: rgb(37, 211, 102);

	--size: 80px;

	width: var(--size);
	height: var(--size);

	position: fixed;
	right: var(--size);
	bottom: var(--size);
	border-radius: 100%;
	border: none;
	z-index: 998;

	cursor: pointer;

	svg {
		margin: 18px;
		fill: #FFF;

		path {
			fill: #FFF;
		}
	}

	@media (max-width: 576px) {

		--size: 70px;
		right: 20px;
		bottom: 20px;
	}
`;

export default function WhatsAppButton()
{
	return <Wrapper onClick={() => window.open("https://api.whatsapp.com/send?phone=551151942000&text=Olá, vim através do site e gostaria de falar com um especialista.")}>
		<WhatsAppIcon />
	</Wrapper>
}