'use client'

import { NavbarComponent } from '@/components/NavBar';
import { DocCard } from '@/components/DocCard';

const docListExample = [
  {
    id: 1,
    docNum: "000000/ABC/2025",
    dateCreateStr: "01/01/2025",
    subject: "Prueba de crear externo con nuevo usuario",
  },
  {
    id: 2,
    docNum: "000000/ABC/2025",
    dateCreateStr: "01/01/2025",
    subject: "Prueba de crear externo con nuevo usuario",
  },
  {
    id: 3,
    docNum: "000000/ABC/2025",
    dateCreateStr: "01/01/2025",
    subject: "Prueba de crear externo con nuevo usuario",
  },
  {
    id: 4,
    docNum: "000000/ABC/2025",
    dateCreateStr: "01/01/2025",
    subject: "Prueba de crear externo con nuevo usuario",
  },
  {
    id: 5,
    docNum: "000000/ABC/2025",
    dateCreateStr: "01/01/2025",
    subject: "Prueba de crear externo con nuevo usuario",
  },
]

export default function Home() {
  return (
    <>
      <NavbarComponent/>
      <section className='flex flex-col gap-4 p-4 md:p-8'>
        <h2 className='text-4xl font-semibold'>Documentos creados recientes</h2>
        <div className='grid grid-cols-1 grid-rows-5 lg:grid-cols-3 lg:grid-rows-2 gap-4'>
          {
            docListExample.map((doc)=>{
              return(
                <DocCard 
                  key={doc.id}
                  id={doc.id}
                  docNum={doc.docNum}
                  dateCreateStr={doc.dateCreateStr}
                  subject={doc.subject}
                />
              )
            })
          }
        </div>

        <h2 className='text-4xl font-semibold'>Documentos asignados recientes</h2>
        <div className='grid grid-cols-1 grid-rows-5 lg:grid-cols-3 lg:grid-rows-2 gap-4'>
          {
            docListExample.map((doc)=>{
              return(
                <DocCard 
                  key={doc.id}
                  id={doc.id}
                  docNum={doc.docNum}
                  dateCreateStr={doc.dateCreateStr}
                  subject={doc.subject}
                />
              )
            })
          }
        </div>
      </section>
    </>
  );
}
