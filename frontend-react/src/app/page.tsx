'use client'

import { NavbarComponent } from '@/components/NavBar';
import {Button} from '@heroui/button'; 

export default function Home() {
  return (
    <>
      <NavbarComponent/>
      Probando
      <Button color='secondary'>Click me</Button>
    </>
  );
}
