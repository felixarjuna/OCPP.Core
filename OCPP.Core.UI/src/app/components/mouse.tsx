import { cn } from "@/lib/utils";
import { motion } from "framer-motion";

interface IMouse extends React.HTMLAttributes<HTMLDivElement> {
  r: number; // radius in rem
  x: number; // x position
  y: number; // y position
  blur?: boolean;
}

export const Mouse = ({ r, x, y, className, blur = true }: IMouse) => {
  return (
    <motion.div
      animate={{ x: x - (r * 16) / 2 - 40, y: y - (r * 16) / 2 - 40 }}
      style={{ width: `${r}rem`, height: `${r}rem` }}
      className={cn(
        `absolute rounded-full bg-gradient-radial from-green-400 to-cyan-400 filter opacity-90 -z-10`,
        className,
        blur ? "blur-3xl" : "-m-8"
      )}
      layoutId="load-animation"
    />
  );
};
