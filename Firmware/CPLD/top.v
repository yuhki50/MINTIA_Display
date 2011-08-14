`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    22:07:38 09/20/2010 
// Design Name: 
// Module Name:    top 
// Project Name: 
// Target Devices: 
// Tool versions: 
// Description: 
//
// Dependencies: 
//
// Revision: 
// Revision 0.01 - File Created
// Additional Comments: 
//
//////////////////////////////////////////////////////////////////////////////////
module top(
	/** pin assign **/
	// input bus //
	ft232rl_data,  // [3:0]
	ft232rl_resx,
	ft232rl_enable,
	ft232rl_a0,
	ft232rl_latch,
	
	// output bus //
	glcd_data,  // [7:0]
	glcd_resx,
	glcd_csx,
	glcd_wrx,
	glcd_rdx,
	glcd_a0
	);
	
	
	/** define pin **/
	// input bus //
	input [3:0] ft232rl_data;  // [3:0]
	input ft232rl_resx;
	input ft232rl_enable;
	input ft232rl_a0;
	input ft232rl_latch;
	
	// output bus //
	output [7:0] glcd_data;  // [7:0]
	output glcd_resx;
	output glcd_csx;
	output glcd_wrx;
	output glcd_rdx;
	output glcd_a0;
	
	
	/** define variable **/
	reg [3:0] data_latch;
	
	
	/** initialize **/
	initial begin
		// variable //
		data_latch <= 0;
	end
	
	
	/** main logic **/
	assign glcd_data = { data_latch, ft232rl_data };  // [7:0]
	assign glcd_resx = ft232rl_resx;
	assign glcd_csx = ft232rl_enable;
	assign glcd_wrx = ft232rl_enable;
	assign glcd_rdx = 1;
	assign glcd_a0 = ft232rl_a0;
	
	
	always @( posedge ft232rl_latch ) begin
		data_latch <= ft232rl_data;
	end
endmodule
