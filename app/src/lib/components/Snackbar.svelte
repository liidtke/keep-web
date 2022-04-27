<script lang="ts">

  export let message:string;
  export let show:boolean;

  $:{
    if(show){
      hide();
    }
  }

  async function hide(){
    console.log('hide func')
    await timeout(4000);
    show = false;
  }

  function timeout(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

</script>

<div id="snackbar" class={show ? 'show' : ''} on:click={() => show = false}  >{message ?? ''}</div>

<style>
  
  #snackbar {
    visibility: hidden;
    min-width: 220px;
    margin-left: -125px;
    background-color: #333;
    color: #fff;
    text-align: center; 
    border-radius: 4px;
    padding: 16px;
    position: fixed;
    z-index: 1;
    left: 50%;
    bottom: 30px;
}

.show {
    visibility: visible !important;
    -webkit-animation: fadein 0.1s, fadeout 0.5s 3.5s;
    animation: fadein 0.1s, fadeout 0.5s 3.5s;
}

@-webkit-keyframes fadein {
    from {bottom: 0; opacity: 0;}
    to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
    from {bottom: 0; opacity: 0;}
    to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
    from {bottom: 30px; opacity: 1;}
    to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
    from {bottom: 30px; opacity: 1;}
    to {bottom: 0; opacity: 0;}
}
</style>