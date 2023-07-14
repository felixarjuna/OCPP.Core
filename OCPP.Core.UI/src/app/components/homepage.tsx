"use client";

import { motion } from "framer-motion";
import React from "react";
import { AnimatedText } from "./(app)/animated-text";
import { ButtonAndLink } from "./(app)/button-and-link";

export const Homepage = () => {
  const [position, setPosition] = React.useState({ x: 0, y: 0 });
  const handleMouseMove = (event: React.MouseEvent) => {
    const { clientX, clientY } = event;
    setPosition({ x: clientX, y: clientY });
  };

  return (
    <div className="-m-[34px]">
      <motion.div onMouseMove={handleMouseMove}>
        <motion.div
          className="absolute w-5 h-5 rounded-full bg-gradient-to-r from-green-400 to-cyan-400 opacity-90 animate-pulse"
          animate={{ x: position.x - 50, y: position.y - 50 }}
        />
      </motion.div>
      <div
        className="flex items-center justify-center flex-col relative overflow-hidden h-[calc(100vh-80px)] space-y-8 rounded-lg"
        onMouseMove={handleMouseMove}
      >
        <AnimatedText />
        <div className="flex justify-center space-y-8">
          <div className="flex gap-8 items-center justify-center">
            <ButtonAndLink />
          </div>
          <div className="absolute top-80 mx-auto w-[100rem] h-[100rem] rounded-full bg-gradient-radial from-green-400 to-cyan-400 filter blur-3xl opacity-90 -z-10"></div>
        </div>
      </div>
    </div>
  );
};
