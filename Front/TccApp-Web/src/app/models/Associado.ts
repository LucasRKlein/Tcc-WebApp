import { UserUpdate } from '@app/models/identity/UserUpdate';
import { SexoType } from '@app/util/enums';

export interface Associado {
  id: number;
  user: UserUpdate;
  imagemUrl: string;

  //#region Dados de pessoa
  nome: string;
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
}
