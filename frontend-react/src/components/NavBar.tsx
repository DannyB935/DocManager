'use client'

import { useState } from "react";

import {  
  Navbar,   
  NavbarBrand,   
  NavbarContent,   
  NavbarItem,   
  NavbarMenuToggle,  
  NavbarMenu,  
  NavbarMenuItem,
  Dropdown,
  DropdownTrigger,
  Button,
  DropdownMenu,
  DropdownItem,
  Accordion,
  AccordionItem,
  Avatar,
  Tooltip
} from "@heroui/react";

import { ChevronDownIcon, ArrowLeftStartOnRectangleIcon } from "@heroicons/react/24/outline";
import Link from "next/link";

export function NavbarComponent(){

  const [isMenuOpen, setIsMenuOpen] = useState<boolean>(false);

  return (
    <Navbar shouldHideOnScroll isBordered onMenuOpenChange={setIsMenuOpen}>

      <NavbarContent justify="start">
        <NavbarMenuToggle
          aria-label={isMenuOpen? 'Cerrar menu': 'Abrir menu'}
          className="sm:hidden"
        />
        <NavbarBrand>
          <p className="text-2xl font-bold">DocManager</p>
        </NavbarBrand>
      </NavbarContent>

      <NavbarContent className="hidden sm:flex gap-4" justify="center">

        {/* Documents menu */}
        <Dropdown>
          <NavbarItem>
            <DropdownTrigger>
              <Button
                disableRipple
                variant="light"
                endContent={<ChevronDownIcon className="size-4"/>}
              >Documentos</Button>
            </DropdownTrigger>
          </NavbarItem>
          <DropdownMenu
            aria-label="Menu de documentos"
          >
            <DropdownItem key="crearDocumento">Crear o ingresar</DropdownItem>
            <DropdownItem key="listadoDocs">Listado</DropdownItem>
          </DropdownMenu>
        </Dropdown>

        {/* Units menu */}
        <Dropdown>
          <NavbarItem>
            <DropdownTrigger>
              <Button
                disableRipple
                variant="light"
                endContent={<ChevronDownIcon className="size-4"/>}
              >
                Unidades
              </Button>
            </DropdownTrigger>
          </NavbarItem>
          <DropdownMenu>
            <DropdownItem key="listaUnidades">Listado</DropdownItem>
            <DropdownItem key="crearUnidad">Crear</DropdownItem>
          </DropdownMenu>
        </Dropdown>

        <Dropdown>
          <NavbarItem>
            <DropdownTrigger>
              <Button
                disableRipple
                variant="light"
                endContent={<ChevronDownIcon className="size-4"/>}
              >
                Usuarios
              </Button>
            </DropdownTrigger>
          </NavbarItem>
          <DropdownMenu>
            <DropdownItem key="listaUsuarios">Listado</DropdownItem>
            <DropdownItem key="crearUsuario">Crear</DropdownItem>
          </DropdownMenu>
        </Dropdown>

      </NavbarContent>

      <NavbarContent justify="end">
        <Dropdown placement="top-end">
          <DropdownTrigger>
            <Avatar as="button" color="primary" className="transition-transform hover:cursor-pointer" name="AB" size="md"/>
          </DropdownTrigger>
          <DropdownMenu aria-label="Opciones del perfil del usuario">
            <DropdownItem key="logout" color="danger">
              <div className="flex gap-2 items-center">
                <ArrowLeftStartOnRectangleIcon className="size-4"/>
                Cerrar sesion
              </div>
            </DropdownItem>
          </DropdownMenu>
        </Dropdown>
      </NavbarContent>

      {/* Mobile menu */}
      <NavbarMenu>
        <Accordion>
            <AccordionItem key="1" aria-label="Opciones del menu de documentos" title="Documentos">
              <NavbarMenuItem>
                <Link href="#">Crear o ingresar</Link>
              </NavbarMenuItem>
              <NavbarMenuItem>
                <Link href='#'>Listado</Link>
              </NavbarMenuItem>
            </AccordionItem>
          
            <AccordionItem key="2" aria-label="Opciones del menu de unidades" title="Unidades">
              <NavbarMenuItem>
                <Link href="#">Crear</Link>
              </NavbarMenuItem>
              <NavbarMenuItem>
                <Link href='#'>Listado</Link>
              </NavbarMenuItem>
            </AccordionItem>
          
            <AccordionItem key="3" aria-label="Opciones del menu de usuarios" title="Usuarios">
              <NavbarMenuItem>
                <Link href="#">Crear</Link>
              </NavbarMenuItem>
              <NavbarMenuItem>
                <Link href='#'>Listado</Link>
              </NavbarMenuItem>
            </AccordionItem>

        </Accordion>

      </NavbarMenu>

    </Navbar>
  )
}