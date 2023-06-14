import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VistoriaImagem } from '@app/models/VistoriaImagem';
import { VistoriaImagemService } from '@app/services/vistoria-imagem.service';
import { StatusCadastroType, OrigemCadastroType } from '@app/util/enums';
import { UtilService } from '@app/util/util.servise';
import { environment } from '@environments/environment';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vistoria-imagem',
  templateUrl: './vistoria-imagem.component.html',
  styleUrls: ['./vistoria-imagem.component.scss']
})
export class VistoriaImagemComponent implements OnInit {

  modalRef: BsModalRef;
  
  veiculoId: string = '';
  associadoId: string = '';
  
  vistoriaImagemId: string = '';
  vistoriaImagem = {} as VistoriaImagem;
  file: File;
  
  //imagem que apresenta no lugar da imagem caso ela não exista
  imagemUrl = 'assets/img/upload.png';


  form: FormGroup;
  estadoSalvar = 'post';

  
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

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
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
    this.carregarVistoriaImagem();
    this.validation();
  }

  public async carregarVistoriaImagem(): Promise<void> {
    //! fazer pegar o veiculo id, e conferir o Id de vistoriaImagem para identificar se é update ou new model
    this.veiculoId = this.activatedRouter.snapshot.paramMap.get('veiculoId');
    this.vistoriaImagemId = this.activatedRouter.snapshot.paramMap.get('id');
    this.form = null
    if (this.vistoriaImagemId !== null && this.vistoriaImagemId !== '') {
      this.spinner.show();
      this.estadoSalvar = 'put';

      this.vistoriaImagemService.getById(this.vistoriaImagemId).subscribe((vistoriaImagem: VistoriaImagem) => {
        this.vistoriaImagem = { ...vistoriaImagem };
        this.form.patchValue(this.vistoriaImagem);
        // this.carregarVistorias();
      },
        (error: any) => {
          this.toastr.error('Erro ao tentar carregar VistoriaImagem.', 'Erro!');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    } else {
      await this.newItemAsync();
    }

    this.initializeForm(this.vistoriaImagem);
  }

  public validation(): void {
    this.form = this.fb.group({
      id: ['', Validators.required],
      veiculoId: ['', Validators.required],
      placa: ['', [
        Validators.required,
        Validators.maxLength(100)]
      ],
      imagemUrl: [''],      
      // statusCadastro: [''],
      // origemCadastro: [''],
      // vistoriaImagems: this.fb.array([]),
    });
  }

  protected initializeForm(model: VistoriaImagem) {
    this.form = this.fb.group({
      id: model.id,
      veiculoId: [model.veiculoId],
      imagemUrl: [model.imagemUrl],
      // statusCadastro: [model.statusCadastro],
      // origemCadastro: [model.origemCadastro],
      // vistoriaImagems: this.fb.array([]),
    });
  }

  async newItemAsync() {
    this.vistoriaImagem.id = this.utilService.newGuid();
    this.vistoriaImagem.veiculoId = this.veiculoId;
    
    this.vistoriaImagem.imagemUrl = "";

    // this.vistoriaImagem.statusCadastro = StatusCadastroType.Aprovado;
    // this.vistoriaImagem.origemCadastro = OrigemCadastroType.SistemaWeb;
  }

  public resetForm(): void {
    this.form.reset();
  }

  cancelarAlteracao() {
    this.router.navigate([`veiculos/detalhe/${this.veiculoId}`]);
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarVistoriaImagem(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.vistoriaImagem = this.estadoSalvar === 'post' ? { ...this.form.value } : { id: this.vistoriaImagem.id, ...this.form.value };
      if (this.estadoSalvar === 'put') {
        this.vistoriaImagemService.edit(this.vistoriaImagem).subscribe((vistoriaImagemRetorno: VistoriaImagem) => {
          if(this.vistoriaImagem.imagemUrl = '') {
            this.imagemUrl = environment.apiURL + 'resources/Vistorias/'+ this.veiculoId +'/'+ this.vistoriaImagem.imagemUrl;
          }
          this.toastr.success('VistoriaImagem salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`veiculos/detalhe/${this.veiculoId}`]);
        },
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('Error ao salvar vistoriaImagem', 'Erro');
          },
          () => this.spinner.hide()
        );
      } else if (this.estadoSalvar === 'post') {
        this.vistoriaImagemService.post(this.vistoriaImagem).subscribe((vistoriaImagemRetorno: VistoriaImagem) => {
          this.toastr.success('VistoriaImagem salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`veiculos/detalhe/${this.veiculoId}`]);
        },
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('Error ao salvar vistoriaImagem', 'Erro');
          },
          () => this.spinner.hide()
        );
      }
    }
    this.spinner.hide();
  }


  editVistoriaImagem(vistoriaImagemId: string) {

  }

  adicionarImagem() {

  }

  public getImagemURL(imagemName: string): string {
    if (imagemName)
      return environment.apiURL + `resources/perfil/${imagemName}`;
    else
      return './assets/img/perfil.png';
  }


  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemUrl = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.vistoriaImagemService.postUploadImagem(this.form.value.id, this.file).subscribe(
      () => {
        this.carregarVistoriaImagem();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

}
