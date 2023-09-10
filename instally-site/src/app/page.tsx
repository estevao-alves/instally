'use client'

import Header from '@/components/header'
import SectionOne from '@/sections/home/SectionOne'
import SectionTwo from '@/sections/home/SectionTwo'
import SectionThree from '@/sections/home/SectionThree'
import SectionFour from '@/sections/home/SectionFour'
import SectionFive from '@/sections/home/SectionFive'
import Footer from '@/components/footer'

export default function Home() {
  return (
    <main>
      <Header />
      <SectionOne />
      <SectionTwo />
      <SectionThree />
      <SectionFour />
      <SectionFive />
      <Footer />
    </main>
  )
}
