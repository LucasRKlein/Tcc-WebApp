import { OrigemCadastroType, StatusCadastroType } from "@app/util/enums";
import { Associado } from "./Associado";

export interface Veiculo {
  id: number;
  placa: string;
  marcaModelo: string;
  anoModelo: string;
  valorFipe: string;
  associadoId: string;
  associado: Associado;
  
  statusCadastro: StatusCadastroType;
  origemCadastro: OrigemCadastroType;
}