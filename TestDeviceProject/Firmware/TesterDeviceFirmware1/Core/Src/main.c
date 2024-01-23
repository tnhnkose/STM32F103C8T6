/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * Copyright (c) 2023 STMicroelectronics.
  * All rights reserved.
  *
  * This software is licensed under terms that can be found in the LICENSE file
  * in the root directory of this software component.
  * If no LICENSE file comes with this software, it is provided AS-IS.
  *
  ******************************************************************************
  */
/* USER CODE END Header */
/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */

/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
 CAN_HandleTypeDef hcan;

I2C_HandleTypeDef hi2c1;

UART_HandleTypeDef huart1;

/* USER CODE BEGIN PV */


	float data=0;  // ADC VARIABLES
	int  AdcValues[12]= {0};
	uint8_t receivedData;
	char buffer[5];
	int value;
	uint8_t bytesToSend[4]; // int, 4 byte'tır

	uint8_t req_counter = 0;  // CAN VARIABLES
	CAN_RxHeaderTypeDef RxHeader;
	int CANOK = 0;
	int counter = 0;


/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_CAN_Init(void);
static void MX_I2C1_Init(void);
static void MX_USART1_UART_Init(void);
/* USER CODE BEGIN PFP */

void Read_ADS1115(int VoltageArray[], int length);
void CAN1_Tx();
void CAN1_Rx(void);
void CAN_Filter_Config(void);

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_CAN_Init();
  MX_I2C1_Init();
  MX_USART1_UART_Init();
  /* USER CODE BEGIN 2 */

  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
	  HAL_UART_Receive(&huart1, &receivedData, 1, HAL_MAX_DELAY);

	     if (receivedData == 'A')
	     {
	     	Read_ADS1115(AdcValues,12);
	     	 for (int i = 0; i < 12; i++)
	     	    {
	     	        value = AdcValues[i];


	     	        // Int değeri byte dizisine dönüştür
	     	        bytesToSend[0] = (value >> 24) & 0xFF;
	     	        bytesToSend[1] = (value >> 16) & 0xFF;
	     	        bytesToSend[2] = (value >> 8) & 0xFF;
	     	        bytesToSend[3] = value & 0xFF;

	     	        // Byte dizisini UART üzerinden gönder
	     	        HAL_UART_Transmit(&huart1, bytesToSend, sizeof(bytesToSend), HAL_MAX_DELAY);
	     	    }
	     }
	     else  if (receivedData == 'C')
	     {
	    	 CAN_Filter_Config();

	    	   	if(HAL_CAN_ActivateNotification(&hcan,CAN_IT_TX_MAILBOX_EMPTY|CAN_IT_RX_FIFO0_MSG_PENDING|CAN_IT_BUSOFF)!= HAL_OK)
	    	   	{
	    	   			Error_Handler();
	    	   	}


	    	   	if( HAL_CAN_Start(&hcan) != HAL_OK)
	    	   	{
	    	   		Error_Handler();
	    	   	}

		 		HAL_UART_Transmit(&huart1, (uint8_t *)"CAN TEST OK\r\n", 13, HAL_MAX_DELAY);


	    	   	while(CANOK == 0 && counter<50)
	    	   	{
	    	   		CAN1_Tx();
	    	   		HAL_Delay(1000);
	    	   		counter++;
	    	   	}

	     }
	     else if(receivedData == 'B')
	     {
	 		HAL_UART_Transmit(&huart1, (uint8_t *)"CIHAZ BEKLEME MODUNDADIR\r\n", 26, HAL_MAX_DELAY);


	     }

	    }
  }
  /* USER CODE END 3 */


/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL9;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }

  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief CAN Initialization Function
  * @param None
  * @retval None
  */
static void MX_CAN_Init(void)
{

  /* USER CODE BEGIN CAN_Init 0 */

  /* USER CODE END CAN_Init 0 */

  /* USER CODE BEGIN CAN_Init 1 */

  /* USER CODE END CAN_Init 1 */
  hcan.Instance = CAN1;
  hcan.Init.Prescaler = 8;
  hcan.Init.Mode = CAN_MODE_NORMAL;
  hcan.Init.SyncJumpWidth = CAN_SJW_1TQ;
  hcan.Init.TimeSeg1 = CAN_BS1_15TQ;
  hcan.Init.TimeSeg2 = CAN_BS2_2TQ;
  hcan.Init.TimeTriggeredMode = DISABLE;
  hcan.Init.AutoBusOff = DISABLE;
  hcan.Init.AutoWakeUp = DISABLE;
  hcan.Init.AutoRetransmission = DISABLE;
  hcan.Init.ReceiveFifoLocked = DISABLE;
  hcan.Init.TransmitFifoPriority = DISABLE;
  if (HAL_CAN_Init(&hcan) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN CAN_Init 2 */

  /* USER CODE END CAN_Init 2 */

}

/**
  * @brief I2C1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_I2C1_Init(void)
{

  /* USER CODE BEGIN I2C1_Init 0 */

  /* USER CODE END I2C1_Init 0 */

  /* USER CODE BEGIN I2C1_Init 1 */

  /* USER CODE END I2C1_Init 1 */
  hi2c1.Instance = I2C1;
  hi2c1.Init.ClockSpeed = 100000;
  hi2c1.Init.DutyCycle = I2C_DUTYCYCLE_2;
  hi2c1.Init.OwnAddress1 = 0;
  hi2c1.Init.AddressingMode = I2C_ADDRESSINGMODE_7BIT;
  hi2c1.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
  hi2c1.Init.OwnAddress2 = 0;
  hi2c1.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
  hi2c1.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;
  if (HAL_I2C_Init(&hi2c1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN I2C1_Init 2 */

  /* USER CODE END I2C1_Init 2 */

}

/**
  * @brief USART1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART1_UART_Init(void)
{

  /* USER CODE BEGIN USART1_Init 0 */

  /* USER CODE END USART1_Init 0 */

  /* USER CODE BEGIN USART1_Init 1 */

  /* USER CODE END USART1_Init 1 */
  huart1.Instance = USART1;
  huart1.Init.BaudRate = 9600;
  huart1.Init.WordLength = UART_WORDLENGTH_8B;
  huart1.Init.StopBits = UART_STOPBITS_1;
  huart1.Init.Parity = UART_PARITY_NONE;
  huart1.Init.Mode = UART_MODE_TX_RX;
  huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart1.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART1_Init 2 */

  /* USER CODE END USART1_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();

}

/* USER CODE BEGIN 4 */

void  Read_ADS1115(int VoltageArray[], int length)
{
	float voltage;
	int lastintValue;

    // channel 1 read
	unsigned char i2c_address= 0x90;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xC2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
	VoltageArray[0] = lastintValue;
	int size = sizeof(voltage);
	lastintValue = 0;
	HAL_Delay(20);

	 // channel 2 read

	i2c_address= 0x90;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xD2;
		buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[1] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);


		 // channel 3 read
	i2c_address= 0x90;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xE2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[2] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);

		 // channel 4 read
	i2c_address= 0x90;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xF2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[3] = lastintValue;
		lastintValue = 0;
		HAL_Delay(10);


		 // channel 5 read
	i2c_address= 0x92;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xC2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[4] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);

	 // channel 6 read
	i2c_address= 0x92;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xD2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[5] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);

	 // channel 7 read
	i2c_address= 0x92;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xE2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[6] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);

	 // channel 8 read
	 i2c_address= 0x92;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xF2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[7] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);

	// channel 9 read
	i2c_address= 0x96;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xC2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[8] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);


	// channel 10 read
	 i2c_address= 0x96;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xD2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[9] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);


		// channel 11 read
	i2c_address= 0x96;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xE2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[10] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);


	// channel 12 read
	i2c_address= 0x96;
	buffer[0]=0x01; // address pointer register: 01 -> config register
	buffer[1]=0xF2;
	buffer[2]=0x85;

	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,3,100);
	buffer[0]=0x00;
	HAL_Delay(10);
	HAL_I2C_Master_Transmit(&hi2c1,i2c_address+0,(uint8_t*)buffer,1,100);
	HAL_Delay(10);
	HAL_I2C_Master_Receive(&hi2c1,i2c_address+1, buffer,2,100);
	data = (buffer[0]<<8) + buffer[1];
	if(data==0xFFFF) {data=0;}
	voltage = data*0.000125;
	lastintValue = 1000*voltage;
		VoltageArray[11] = lastintValue;
		lastintValue = 0;
		HAL_Delay(20);


	return 0;

}

uint8_t led_no=0;

void CAN1_Tx(void)
{
	CAN_TxHeaderTypeDef TxHeader;

	uint32_t TxMailbox;

	uint8_t ourMessage[5] = {'H','E','L','L','O'};
	TxHeader.DLC = 5;
	TxHeader.StdId = 0x65D;
	TxHeader.IDE = CAN_ID_STD;
	TxHeader.RTR = CAN_RTR_DATA;

	if(HAL_CAN_AddTxMessage(&hcan,&TxHeader,ourMessage,&TxMailbox) != HAL_OK)
	{
		Error_Handler();
	}



}

void CAN_Filter_Config(void)
{
	CAN_FilterTypeDef can1_filter_init;

	can1_filter_init.FilterActivation = ENABLE;
	can1_filter_init.FilterBank  = 0;
	can1_filter_init.FilterFIFOAssignment = CAN_RX_FIFO0;
	can1_filter_init.FilterIdHigh = 0x0000;
	can1_filter_init.FilterIdLow = 0x0000;
	can1_filter_init.FilterMaskIdHigh = 0X01C0;
	can1_filter_init.FilterMaskIdLow = 0x0000;
	can1_filter_init.FilterMode = CAN_FILTERMODE_IDMASK;
	can1_filter_init.FilterScale = CAN_FILTERSCALE_32BIT;

	if( HAL_CAN_ConfigFilter(&hcan,&can1_filter_init) != HAL_OK)
	{
		Error_Handler();
	}

}

void CAN1_Rx()

{
	CAN_RxHeaderTypeDef RxHeader;

	uint8_t rcvdMsg[5];

	char msg[50];



	if(HAL_CAN_GetRxMessage(&hcan, CAN_RX_FIFO0, &RxHeader, rcvdMsg) != HAL_OK )
	{
		Error_Handler();
	}

    sprintf(msg,"Message Received: %s\r\n",rcvdMsg);


}


void HAL_CAN_RxFifo0MsgPendingCallback(CAN_HandleTypeDef *hcan)
{
	uint8_t rcvd_msg[8];

	char msg[50];

	if(HAL_CAN_GetRxMessage(hcan,CAN_RX_FIFO0,&RxHeader,rcvd_msg) != HAL_OK)
	{
		Error_Handler();
	}

	if(RxHeader.StdId == 0x65D && RxHeader.RTR == 0 )
	{
		//This is data frame sent by n1 to n2
		HAL_UART_Transmit(&huart1, (uint8_t *)"CAN TEST OK\r\n", 13, HAL_MAX_DELAY);
		CANOK = 1;
	}
	else if ( RxHeader.StdId == 0x651 && RxHeader.RTR == 1)
	{
		//This is a remote frame sent by n1 to n2
		Send_response(RxHeader.StdId);
		return;
	}
	else if ( RxHeader.StdId == 0x651 && RxHeader.RTR == 0)
	{
		//its a reply ( data frame) by n2 to n1
	    HAL_UART_Transmit(&huart1, (uint8_t *)"CAN TEST OK\r\n", 13, HAL_MAX_DELAY);
	    CANOK = 1;
	}

	 HAL_UART_Transmit(&huart1,(uint8_t*)msg,strlen(msg),HAL_MAX_DELAY);

}

void Send_response(uint32_t StdId)
 {

 	CAN_TxHeaderTypeDef TxHeader;

 	uint32_t TxMailbox;

 	uint8_t response[2] = { 0xAB,0XCD};

 	TxHeader.DLC = 2;
 	TxHeader.StdId = StdId;
 	TxHeader.IDE   = CAN_ID_STD;
 	TxHeader.RTR = CAN_RTR_DATA;

 	if( HAL_CAN_AddTxMessage(&hcan,&TxHeader,response,&TxMailbox) != HAL_OK)
 	{
 		Error_Handler();
 	}

 }



/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  __disable_irq();
  while (1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */
