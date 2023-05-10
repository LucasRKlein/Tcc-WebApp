import { OrigemCadastroType, SexoType, StatusCadastroType } from '@app/util/enums';
import { Veiculo } from './Veiculo';

export interface Associado {
  id: number;
  
  //#region Dados de pessoa
  nome: string;
  imagemUrl: string;
  cpf: string;
  sexo: SexoType;
  dataNascimento: Date;
  celular: string;
  email: string;
  //#endregion

  //#region Dados de endereço
  ruaAvenida: string;
  numero: string;
  complemento: string;
  bairro: string;
  cep: string;
  estadoNome: string;
  cidadeNome: string;
  //#endregion

  veiculos: Veiculo[];

  statusCadastro: StatusCadastroType;
  origemCadastro: OrigemCadastroType;
}
