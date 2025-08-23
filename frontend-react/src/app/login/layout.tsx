export default function LoginLayout({children}: { children: React.ReactNode }) {
  return (
    <div className="min-h-dvh flex flex-col justify-center content-center items-center">
      {children}
    </div>
  );
};