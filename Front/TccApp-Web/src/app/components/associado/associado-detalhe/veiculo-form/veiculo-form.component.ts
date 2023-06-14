import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Veiculo } from '@app/models/Veiculo';
import { VistoriaImagem } from '@app/models/VistoriaImagem';
import { VeiculoService } from '@app/services/veiculo.service';
import { OrigemCadastroType, StatusCadastroType } from '@app/util/enums';
import { UtilService } from '@app/util/util.servise';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from '@environments/environment';
import { VistoriaImagemService } from '@app/services/vistoria-imagem.service';

@Component({
  selector: 'app-veiculo-form',
  templateUrl: './veiculo-form.component.html',
  styleUrls: ['./veiculo-form.component.scss']
})
export class VeiculoFormComponent implements OnInit {

  modalRef: BsModalRef;
  associadoId: string = '';
  veiculoId: string = '';
  veiculo = {} as Veiculo;
  form: FormGroup;
  estadoSalvar = 'post';

  listaVistoriaImagem: VistoriaImagem[] = [];
  public larguraImagem = 150;
  public margemImagem = 2;
  //imagem que apresenta no lugar da imagem caso ela não exista
  imagemUrlPadrao = 'assets/img/upload.png';
  public file: File;

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

  statusCadastroTypes = [
    { text: 'Aprovado', value: StatusCadastroType.Aprovado, color: 'bg-green', icon: 'pending' },
    { text: 'Cancelado', value: StatusCadastroType.Cancelado, color: 'bg-red', icon: 'done' },
    { text: 'Pré Cadastro', value: StatusCadastroType.PreCadastro, color: 'bg-orange', icon: 'close' }
  ];

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private veiculoService: VeiculoService,
    private vistoriaImagemService: VistoriaImagemService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router,
    readonly utilService: UtilService
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarVeiculo();
    this.validation();
  }

  public async carregarVeiculo(): Promise<void> {
    //! fazer pegar o associado id, e conferir o Id de veiculo para identificar se é update ou new model
    this.associadoId = this.activatedRouter.snapshot.paramMap.get('associadoId');
    this.veiculoId = this.activatedRouter.snapshot.paramMap.get('id');
    this.form = null
    if (this.veiculoId !== null && this.veiculoId !== '') {
      this.spinner.show();
      this.estadoSalvar = 'put';

      this.veiculoService.getById(this.veiculoId).subscribe((veiculo: Veiculo) => {
        this.veiculo = { ...veiculo };
        this.form.patchValue(this.veiculo);
        this.carregarVistoriaImagens();
      },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar Veiculo.', 'Erro!');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    } else {
      await this.newItemAsync();
    }

    this.initializeForm(this.veiculo);
  }

  public validation(): void {
    this.form = this.fb.group({
      id: ['', Validators.required],
      associadoId: ['', Validators.required],
      placa: ['', [
        Validators.required,
        Validators.maxLength(100)]
      ],
      marcaModelo: [''],
      anoModelo: [''],
      valorFipe: [''],
      statusCadastro: [''],
      origemCadastro: [''],
      // veiculos: this.fb.array([]),
    });
  }

  protected initializeForm(model: Veiculo) {
    this.form = this.fb.group({
      id: model.id,
      associadoId: [model.associadoId],
      placa: [model.placa, [
        Validators.required,
        Validators.maxLength(100)]
      ],
      marcaModelo: [model.marcaModelo],
      anoModelo: [model.anoModelo],
      valorFipe: [model.valorFipe],
      statusCadastro: [model.statusCadastro],
      origemCadastro: [model.origemCadastro],
      // veiculos: this.fb.array([]),
    });
  }

  async newItemAsync() {
    this.veiculo.placa = 'ABC-1234';
    this.veiculo.id = this.utilService.newGuid();
    this.veiculo.associadoId = this.associadoId;
    this.veiculo.marcaModelo = '';
    this.veiculo.anoModelo = '';
    this.veiculo.valorFipe = '';

    this.veiculo.statusCadastro = StatusCadastroType.Aprovado;
    this.veiculo.origemCadastro = OrigemCadastroType.SistemaWeb;
  }

  public resetForm(): void {
    this.form.reset();
  }

  cancelarAlteracao() {
    this.router.navigate([`associados/detalhe/${this.associadoId}`]);
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarVeiculo(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.veiculo = this.estadoSalvar === 'post' ? { ...this.form.value } : { id: this.veiculo.id, ...this.form.value };
      if (this.estadoSalvar === 'put') {
        this.veiculoService.edit(this.veiculo).subscribe((veiculoRetorno: Veiculo) => {
          this.toastr.success('Veiculo salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`associados/detalhe/${this.associadoId}`]);
        },
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('Error ao salvar veiculo', 'Erro');
          },
          () => this.spinner.hide()
        );
      } else if (this.estadoSalvar === 'post') {
        this.veiculoService.post(this.veiculo).subscribe((veiculoRetorno: Veiculo) => {
          this.toastr.success('Veiculo salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`associados/detalhe/${this.associadoId}`]);
        },
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('Error ao salvar veiculo', 'Erro');
          },
          () => this.spinner.hide()
        );
      }
    }
    this.spinner.hide();
  }

  public async carregarVistoriaImagens(): Promise<void> {
    let retorno = await this.vistoriaImagemService.getVistoriaImagemsByVeiculoId(this.veiculoId).toPromise();

    if (retorno.length > 0) {
      this.listaVistoriaImagem = [...retorno];
      this.toastr.success('Imagens Vistoria carregadas', 'Sucesso');
    } else if (retorno.length = 0) {
      this.toastr.info('Veiculo não possui Imagens Vistoria', 'Atenção');
    }
  }

  // editVistoriaImagem(vistoriaImagemId: string) {
  //   this.uploadImagem(vistoriaImagemId);
  // }

  onFileChange(ev: any, vistoriaImagemId: string): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemUrlPadrao = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem(vistoriaImagemId);
  }

  uploadImagem(vistoriaImagemId: string): void {
    this.spinner.show();
    this.vistoriaImagemService.postUploadImagem(vistoriaImagemId, this.file).subscribe(
      () => {
        this.carregarVistoriaImagens();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

  adicionarImagem() {
    let novaVistoriaImagem= {} as VistoriaImagem;
    novaVistoriaImagem.id = this.utilService.newGuid();
    novaVistoriaImagem.veiculoId = this.veiculoId;
    novaVistoriaImagem.imagemUrl = '';
    this.vistoriaImagemService.post(novaVistoriaImagem).subscribe((vistoriaImagemRetorno: VistoriaImagem) => {
      this.toastr.success('Vistoria Imagem salvo com Sucesso!', 'Sucesso');
      this.listaVistoriaImagem.push(novaVistoriaImagem);
    },
      (error: any) => {
        console.error(error);
        this.spinner.hide();
        this.toastr.error('Error ao salvar Vistoria Imagem', 'Erro');
      },
      () => this.spinner.hide()
    );
  }

  async deleteImagem(vistoriaImagemId: string) {
    let retorno = await this.vistoriaImagemService.deleteVistoriaImagem(vistoriaImagemId).toPromise();
    if(retorno !== null) {
      this.toastr.success('Vistoria Imagem deletada com sucesso', 'Sucesso');
      this.listaVistoriaImagem.filter(x => x.id !== vistoriaImagemId);
    }
  }

  public getImagemURL(imagemUrl: string): string {
    if (imagemUrl) {
      return environment.apiURL + `resources/Vistorias/${imagemUrl}`;        
    } else {
      return './assets/img/perfil.png';
    }
  }
}
