import { styled } from "styled-components";

export const UploadWrapper = styled.div`
	width: 100%;
	background: #f2f2f2;
	height: 160px;
	border-radius: 10px;

	position: relative;

	#overlay {
		width: 100%;
		height: 100%;
		background: transparent;
		position: absolute;
		left: 0;
		top: 0;
		border-radius: 10px;
		cursor: pointer;
	}

	#inputFile { display: none; }

	.content {
		height: 100%;
		border-radius: 8px;
		padding: 30px;

		user-select: none;
		-o-user-select: none;
		-moz-user-select: none;
		-webkit-user-select: none;

		border: 3px dashed #868182;

		transition: 200ms ease-in-out;

		text-align: center;
		display: flex;
		align-items: center;
		justify-content: center;

		&.active {
			background: rgba(0,0,0,.1);
		}

		svg {
			height: 80px;
			margin: 0 30px 0 0;
		}

		h4 {
			font-size: 20px;
			margin: 0 0 10px;
		}

		button {
			font-size: 16px;
			font-weight: 600;
			color: #333;
			background: #D6D6D6;
			padding: 10px 20px;
			border-radius: 10px;
		}

	}
	
	@media (max-width: 768px) {
		height: auto;
		
		.content {
			flex-direction: column;

			svg {
				margin: 0 0 20px;
			}

			.text {
				display: flex;
				flex-direction: column;
				align-items: center;

				h4 { display: none; }
			}
		}
	}
`;

export const Previewer = styled.div`
	width: 100%;
	height: 100%;
	border-radius: 8px;
	background-color: #f1f1f1;
	position: relative;
	overflow: hidden;
	background-image: url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAiIGhlaWdodD0iNDAiIHZpZXdCb3g9IjAgMCA0MCA0MCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPGcgY2xpcC1wYXRoPSJ1cmwoI2NsaXAwXzY0Ml80MDkxKSI+CjxwYXRoIGQ9Ik0yMCAwSDBWMjBIMjBWMFoiIGZpbGw9IiNFN0U3RTciLz4KPHBhdGggZD0iTTQwIDIwSDIwVjQwSDQwVjIwWiIgZmlsbD0iI0U3RTdFNyIvPgo8L2c+CjxkZWZzPgo8Y2xpcFBhdGggaWQ9ImNsaXAwXzY0Ml80MDkxIj4KPHJlY3Qgd2lkdGg9IjQwIiBoZWlnaHQ9IjQwIiBmaWxsPSJ3aGl0ZSIvPgo8L2NsaXBQYXRoPgo8L2RlZnM+Cjwvc3ZnPgo=');

	img {
		z-index: 1;
		width: 100%;
		position: absolute;
		margin: auto;
		top: 0;
		bottom: 0;

		background-size: contain;
		background-position: center;
		background-repeat: no-repeat;
	}
`;

export const PreviewerActions = styled.div`
	display: grid;
	grid-template-columns: 1fr 1fr;
	grid-gap: 10px;
	margin: 20px 0 0;

	button {
		max-width: 100%;
		font-size: 20px;
		height: 60px;

		&.cancel {
			background: rgba(0,0,0,.2);
		}
	}
`;

export const PreviewerPDF = styled.div`
	height: 100%;
	margin: 10px;
	
	display: flex;
	align-items: center;
	
	.item {
		padding: 10px;
		border-radius: 10px;
		background: #8e555422;

		display: flex;
		justify-content: center;
		align-items: center;
		border: 4px solid #8e555477;
		
		.icon {
			margin-right: 20px;
			border-radius: 10px;
			
			--wrappericon-size: 56px;

			min-width: var(--wrappericon-size);
			max-width: var(--wrappericon-size);
			height: var(--wrappericon-size);
			
			background: #8e5554;

			display: flex;
			justify-content: center;
			align-items: center;

			&::after {
				content: "PDF";
				font-size: 16px;
				color: #FFF;
			}
		}
	}
`;