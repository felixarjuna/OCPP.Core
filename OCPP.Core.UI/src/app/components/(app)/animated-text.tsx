"use client";

import { motion } from "framer-motion";
import Typewriter from "./typewriter";

const paragraphVariants = {
  initial: { y: 300, opacity: 0 },
  animate: {
    y: 0,
    opacity: 1,
    transition: {
      delay: 2,
      y: { ease: [0.6, 0.01, -0.05, 0.95], duration: 0.8 },
    },
  },
  exit: {},
};

export const AnimatedText = () => {
  return (
    <div className="-mt-16 flex items-center flex-col space-y-8">
      <Typewriter words={["Welcome to Ploeg!"]} />
      <motion.p
        variants={paragraphVariants}
        initial={"initial"}
        animate={"animate"}
        exit={"exit"}
        className="font-extralight w-2/3 text-center text-lg"
      >
        A place where all charge stations find its home. Empowering the future of mobility with
        clean energy without damaging the environment.
      </motion.p>
    </div>
  );
};
