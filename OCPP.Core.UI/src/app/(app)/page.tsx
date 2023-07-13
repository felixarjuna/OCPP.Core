import { Leaf } from "lucide-react";
import { ButtonAndLink } from "../components/(app)/button-and-link";

export default async function Home() {
  return (
    <main className="mt-8 flex items-center justify-center h-full flex-col space-y-8 relative overflow-hidden">
      <div className="-mt-16 flex items-center flex-col space-y-5">
        <h1 className="text-5xl gap-3 flex">
          Welcome to Ploeg!
          <Leaf className="w-10 h-10" />
        </h1>
        <p className="font-extralight w-2/3 text-center">
          A place where all charge stations find its home. Empowering the future of mobility with
          clean energy without damaging the environment.
        </p>
      </div>
      <div className="flex gap-8 items-center justify-center">
        <ButtonAndLink />
      </div>
      <div className="absolute top-80 mx-auto w-[100rem] h-[100rem] rounded-full bg-gradient-radial from-green-400 to-cyan-400 filter blur-3xl opacity-90 -z-10"></div>
    </main>
  );
}
