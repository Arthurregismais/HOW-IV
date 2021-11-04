-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 04-Nov-2021 às 02:11
-- Versão do servidor: 10.4.20-MariaDB
-- versão do PHP: 8.0.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `mysql`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `carro`
--

CREATE TABLE `carro` (
  `idCarro` int(11) NOT NULL,
  `nomeCarro` varchar(50) NOT NULL,
  `marcaCarro` varchar(50) NOT NULL,
  `descriçãoCarro` text NOT NULL,
  `anoCarro` int(4) NOT NULL,
  `ativoCarro` int(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `carro`
--

INSERT INTO `carro` (`idCarro`, `nomeCarro`, `marcaCarro`, `descriçãoCarro`, `anoCarro`, `ativoCarro`) VALUES
(1, 'Kwid', 'Renault', 'Carro em estado seminovo, confiável, cromado, com motor de 70 cavalos em ótimas condições. Câmbio manual de 5 marchas. Ideal para um casal. ', 2020, 1),
(2, 'Mobi', 'Fiat', 'Compacto, econômico e confiável. Está pintado de azul marinho com adesivos de corrida do Ayrton Senna. Motor modificado de 80 cavalos. Câmbio manual de 5 marchas. AVISO IMPORTANTE: Sua arrancada pode ser um pouco violenta nas mãos de motoristas inexperientes, tratar com cuidado. ', 2021, 1),
(3, 'Ecosport', 'Ford', 'SUV confortável, ideal para família, com um grande porta malas que suporta 356 litros. Cromado e pronto para viagens em familiares. Seu motor é de 137 cavalos de potência. Câmbio automático de 6 marchas.', 2019, 1),
(4, 'Logan', 'Renault', 'Sedan vermelho 4 portas, confortável, econômico, com motor de 115 cavalos e câmbio manual de 5 marchas. Obs: Apesar de ele servir muito bem para assaltos, esta prática não é incentivada e muito menos aconselhada por nossa empresa', 2021, 1),
(5, 'QQ', 'Chery', 'Sub compacto na cor amarelo, que apesar da idade ainda serve muito bem seu propósito. Com um motor muito econômico e potência de 68 cavalos. Câmbio manual de 5 marchas. Não é recomendado para uso familiar, apenas para locomoção na cidade. Graças a seu tamanho diminuto é muito fácil de estacionar em qualquer lugar. ', 2012, 1),
(6, 'Fox ', 'Ford', 'Seminovo, em boas condições', 2014, 1);

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `carro`
--
ALTER TABLE `carro`
  ADD PRIMARY KEY (`idCarro`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `carro`
--
ALTER TABLE `carro`
  MODIFY `idCarro` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
