'use client'

import {
  Input,
  Button,
} from "@heroui/react";

import { useForm, SubmitHandler } from "react-hook-form"
import * as z from "zod";
import {zodResolver} from "@hookform/resolvers/zod";

import {redirect} from 'next/navigation';

interface LoginFormValues {
  code: string;
  password: string;
}

const LoginSchema = z.object({
  code: z.string()
    .trim()
    .length(9, "Su codigo debe tener 9 digitos")
    .regex(/^[0-9]+$/, "El código solo debe contener números"),
  password: z.string().min(3, "Su contraseña no puede tener menos de 3 caracteres"),
});

export default function LoginPage(){

  const {
    register,
    handleSubmit,
    formState: {errors, isValid, isSubmitting}
  } = useForm<LoginFormValues>({
    resolver: zodResolver(LoginSchema),
    mode: "onTouched"
  });

  const onSubmit: SubmitHandler<LoginFormValues> = async(data) => {
    await new Promise(resolve => setTimeout(resolve, 3000));
    console.log(data);
  }

  return (
    <div className="bg-gray-200 rounded-lg shadow-md px-8 py-4 w-full lg:w-1/5">

      <div className="my-8">
        <h2 className="font-bold text-4xl text-center">Iniciar Sesion</h2>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="mt-4">
        <div className="flex flex-col gap-4 mb-4">
          <Input
            {...register("code", {required: true})}
            isInvalid={errors.code? true: false }
            errorMessage={errors.code?.message}
            isRequired
            label="Código"
            type="text"
            variant="faded"
          />
          <Input
            {...register("password" , {required: true})}
            isInvalid={errors.password? true : false}
            errorMessage={errors.password?.message}
            isRequired
            label="Contraseña"
            type="password"
            variant="faded"
          />
        </div>

        <div className="flex gap-4">
          <Button 
            color="primary"
            className="w-1/2" 
            type="submit"
            isDisabled={!isValid}
            isLoading={isSubmitting}
          >Ingresar</Button>
          <Button
            color="secondary"
            className="w-1/2"
            type="button"
            isDisabled={isSubmitting}
            onPress={()=>redirect('/')}
          >
            Cancelar
          </Button>
        </div>
      </form>

    </div>
  )
}