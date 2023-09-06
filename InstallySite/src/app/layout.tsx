import type { Metadata } from 'next'
import { Poppins } from 'next/font/google'
import configs from '../../configs';
import Script from 'next/script';

const font = Poppins({ subsets: ["latin"], weight: ["300","500","600","700","800","900"], display: 'swap', variable: '--font-poppins', });

export const metadata: Metadata = {
  metadataBase: new URL(configs.domain),
  title: 'CriarPagina.com - Sua página profissional na internet',
  description: 'Profissionais de todas as áreas, professores e especialistas criam suas páginas aqui. A página que você precisa para anunciar seu produto ou serviço na internet.',
  alternates: {
    canonical: '/',
  },
  openGraph: {
    title: "Sua página profissional na internet",
    description: "Profissionais de todas as áreas, professores e especialistas criam suas páginas aqui.",
    images: "/og-image-main.png",
    url: configs.domain,
    type: "website"
  },
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {

  return (
    <html lang="pt-Br" className={font.className}>
      <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <Script async src="https://www.googletagmanager.com/gtag/js?id=G-NBFSYES1D5" />
        <Script
          id="gtm-script"
          strategy="afterInteractive"
          dangerouslySetInnerHTML={{ __html: `
            window.dataLayer = window.dataLayer || [];
            function gtag(){dataLayer.push(arguments);}
            gtag('js', new Date());
            gtag('config', 'G-NBFSYES1D5');
          ` }} />
      </head>
      <body>
        {children}
      </body>
    </html>
  )
}
