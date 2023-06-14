import { OrigemCadastroType, StatusCadastroType } from "@app/util/enums";
import { Associado } from "./Associado";
import { Veiculo } from "./Veiculo";

export interface VistoriaImagem {
  id: string;
  veiculoId: string;
  veiculo: Veiculo;
  imagemUrl: string;
  // ImagemBase64: string;
}