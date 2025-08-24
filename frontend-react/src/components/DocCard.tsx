'use client'

import {
  Card, 
  CardHeader, 
  CardBody, 
  CardFooter,
  Button
} from "@heroui/react";

import { EyeIcon } from "@heroicons/react/24/outline";
import { redirect } from "next/navigation";

export function DocCard(props: DocCardProps){
  const {id, docNum, dateCreateStr, subject} = props;
  return (
    <Card isBlurred className="p-4 bg-blue-300/60 border-none" shadow="md">
      <CardHeader className="flex-col items-start">
        <p className="font-bold">{docNum}</p>
        <small className="text-default-500">Fecha creacion: {dateCreateStr}</small>
      </CardHeader>
      <CardBody>
        <p className="truncate font-semibold">{subject}</p>
      </CardBody>
      <CardFooter>
        <Button 
          color="secondary"
          endContent={<EyeIcon className="size-8"/>}
          size="md"
          variant="shadow"
          onPress={()=> redirect(`/#/${id}`)}
        >
          Detalle
        </Button>
      </CardFooter>
    </Card>
  );
}

interface DocCardProps{
  id: number;
  docNum: string;
  dateCreateStr: string;
  subject: string;
}