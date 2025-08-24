import { Inter } from 'next/font/google'
import "./globals.css";

import {Providers} from "./providers";

import type { Metadata } from 'next'

export const metadata: Metadata = {
  title: 'DocManager',
  description: 'DocManager system developed on React and Next.js',
}

const inter = Inter({ subsets: ['latin'] });

export default function RootLayout({children}: { children: React.ReactNode }) {
  return (
    <html lang="en" className={`${inter.className} light`}>
      <body>
        <Providers>
          <div className="min-h-dvh flex flex-col">
            {children}
          </div>
        </Providers>
      </body>
    </html>
  );
}
