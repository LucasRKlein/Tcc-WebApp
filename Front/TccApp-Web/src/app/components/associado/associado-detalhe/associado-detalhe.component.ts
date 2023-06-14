import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
import { environment } from '@environments/environment';
import { Associado } from '@app/models/Associado';
import { AssociadoService } from '@app/services/associado.service';
import { Veiculo } from '@app/models/Veiculo';
import { VeiculoService } from '@app/services/veiculo.service';
import { UtilService } from '@app/util/util.servise';
import { OrigemCadastroType, SexoType, StatusCadastroType } from '@app/util/enums';

@Component({
  selector: 'app-associado-detalhe',
  templateUrl: './associado-detalhe.component.html',
  styleUrls: ['./associado-detalhe.component.scss'],
  providers: [DatePipe],
})
export class AssociadoDetalheComponent implements OnInit {
  modalRef: BsModalRef;
  associadoId: string = '';
  associado = {} as Associado;
  form: FormGroup;
  veiculoAtualId: string = '';
  veiculoAtual = { id: '', marcaModelo: '', indice: 0 };
  estadoSalvar = 'post';
  imagemURL = 'assets/img/upload.png';
  file: File;


  listaVeiculos: Veiculo[] = [];

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  sexoTypes = [
    { text: 'Não Definido', value: SexoType.NaoDefinido },
    { text: 'Masculino', value: SexoType.Masculino },
    { text: 'Feminino', value: SexoType.Feminino }
  ];

  statusCadastroTypes = [
    { text: 'Aprovado', value: StatusCadastroType.Aprovado, color: 'bg-green', icon: 'pending' },
    { text: 'Cancelado', value: StatusCadastroType.Cancelado, color: 'bg-red', icon: 'done' },
    { text: 'Pré Cadastro', value: StatusCadastroType.PreCadastro, color: 'bg-orange', icon: 'close' }
  ];

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private associadoService: AssociadoService,
    private veiculoService: VeiculoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router,
    private datePipe: DatePipe,
    readonly utilService: UtilService
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarAssociado();
    this.validation();
  }

  public async carregarAssociado(): Promise<void> {
    this.associadoId = this.activatedRouter.snapshot.paramMap.get('id');
    this.form = null
    if (this.associadoId !== null && this.associadoId !== '') {
      this.spinner.show();
      this.estadoSalvar = 'put';

      this.associadoService.getAssociadoById(this.associadoId).subscribe((associado: Associado) => {
        this.associado = { ...associado };
        this.form.patchValue(this.associado);
        if (this.associado.imagemUrl !== '') {
          this.imagemURL = environment.apiURL + 'resources/images/' + this.associado.imagemUrl;
        }
        this.carregarVeiculos();
      },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar Associado.', 'Erro!');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    } else {
      console.log(this.form);
      await this.newItemAsync();
    }

    this.initializeForm(this.associado);
  }

  public validation(): void {
    this.form = this.fb.group({
      id: ['', Validators.required],
      nome: ['', Validators.maxLength(100)],
      imagemUrl: [''],
      cpf: [''],
      sexo: [''],
      dataNascimento: [''],
      celular: [''],
      email: ['', Validators.email],
      ruaAvenida: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: ['', Validators.required],
      bairro: ['', [Validators.required]],
      estadoNome: ['', [Validators.required]],
      cidadeNome: ['', [Validators.required]],
      statusCadastro: [''],
      origemCadastro: [''],
      veiculos: this.fb.array([]),
    });
  }

  protected initializeForm(model: Associado) {
    this.form = this.fb.group({
      id: model.id,
      nome: [model.nome, [
        Validators.required,
        Validators.maxLength(100)]
      ],
      imagemUrl: [model.imagemUrl],
      cpf: [model.cpf],
      sexo: [model.sexo],
      dataNascimento: [this.utilService.fromJsonDate(model.dataNascimento)],
      celular: [model.celular],
      email: [model.email, Validators.email],
      ruaAvenida: [model.ruaAvenida, Validators.required],
      numero: [model.numero, Validators.required],
      complemento: [model.complemento, Validators.required],
      bairro: [model.bairro, [Validators.required]],
      estadoNome: [model.estadoNome, [Validators.required]],
      cidadeNome: [model.cidadeNome, [Validators.required]],
      statusCadastro: [model.statusCadastro],
      origemCadastro: [model.origemCadastro],
      veiculos: this.fb.array([]),
    });
  }

  async newItemAsync() {
    this.associado.nome = 'Joãozinho da silva';
    this.associado.dataNascimento = new Date();
    this.associado.sexo = SexoType.NaoDefinido;
    this.associado.id = this.utilService.newGuid();
    this.associado.statusCadastro = StatusCadastroType.Aprovado;
    this.associado.origemCadastro = OrigemCadastroType.SistemaWeb;
  }

  public resetForm(): void {
    this.form.reset();
  }

  cancelarAlteracao() {
    this.router.navigate([`associados`]);
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarAssociado(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.associado = this.estadoSalvar === 'post' ? { ...this.form.value } : { id: this.associado.id, ...this.form.value };
      this.associadoService[this.estadoSalvar](this.associado).subscribe((associadoRetorno: Associado) => {
        this.toastr.success('Associado salvo com Sucesso!', 'Sucesso');
        // this.router.navigate([`associados/detalhe/${associadoRetorno.id}`]);
        this.router.navigate([`associados`]);
      },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error ao salvar associado', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
    this.spinner.hide();
  }

  async autosave(): Promise<boolean> {
    if (this.form.valid) {
      this.associado = this.estadoSalvar === 'post' ? { ...this.form.value } : { id: this.associado.id, ...this.form.value };
      this.associadoService[this.estadoSalvar](this.associado).subscribe((associadoRetorno: Associado) => {
        this.toastr.success('Associado salvo com Sucesso!', 'Sucesso');
        return true;
      },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error ao salvar associado', 'Erro');
        },
        () => this.spinner.hide()
      );
    } else {
      return false;
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.associadoService.postUpload(this.associadoId, this.file).subscribe(
      () => {
        this.carregarAssociado();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

  public async carregarVeiculos(): Promise<void> {
    let retorno = await this.veiculoService.getVeiculosByAssociadoId(this.associadoId).toPromise();

    if (retorno.length > 0) {
      this.listaVeiculos = [...retorno];
      this.toastr.success('Veiculos carregados', 'Sucesso');
    } else if (retorno.length = 0) {
      this.toastr.info('Associado não possui Veiculos', 'Atenção');
    }
  }

  public async adicionarVeiculo() {
    await this.autosave();
    this.router.navigate([`veiculo/${this.associadoId}`]);
  }

  public async editVeiculo(veiculoId: string) {
    await this.autosave();
    this.router.navigate([`veiculo/${this.associadoId}/${veiculoId}`]);
  }
  
  openModal(event: any, template: TemplateRef<any>, veiculoId: string): void {
    event.stopPropagation();
    this.veiculoAtualId = veiculoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  async confirmDeleteVeiculo() {
    this.modalRef.hide();
    this.spinner.show();

    let retorno = await this.veiculoService.deleteVeiculo(this.veiculoAtualId).toPromise();
    if(retorno !== null) {
      this.toastr.success('Veiculo deletado com sucesso', 'Sucesso');
      this.listaVeiculos.filter(x => x.id !== this.veiculoAtualId);
      // this.listaVeiculos = await this.veiculoService.getVeiculosByAssociadoId(this.associadoId).toPromise();
    }

    this.spinner.hide();
    this.veiculoAtualId = '';
  }

  declineDeleteVeiculo(): void {
    this.modalRef.hide();
  }

  getStatusCadastroTypeNome(tipo: StatusCadastroType): string {
    return this.statusCadastroTypes.find(x => x.value === tipo)?.text;
  }
  
  getStatusCadastroTypeColor(tipo: StatusCadastroType): string {
    return this.statusCadastroTypes.find(x => x.value === tipo)?.color;
  }
}
