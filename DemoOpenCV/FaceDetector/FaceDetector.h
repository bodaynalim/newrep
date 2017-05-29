#pragma once

#include "stdafx.h"

using namespace std;
using namespace cv;

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Drawing;
using namespace System::Text;

namespace SNFaceCrop {

public ref class FaceDetector
{

	private:
		double scale;
		bool imageIsOpened;
		CascadeClassifier * cascade;
		cv::String * cascadeName;
		cv::String * inputName;
		cv::Size * imageSize;
		List<System::Drawing::Rectangle> ^ faces;

	private:

		void marshalString ( System::String ^ s, cv::String * cvs );
		void detectCascadeClassifier( Mat& img,
														 CascadeClassifier& cascade,
														 double scale,
														 vector<Rect>& detectedFaces
														 );
		List<System::Drawing::Rectangle> ^ getListOfRectangle( vector<Rect> &rectVect );

	public:

		FaceDetector(void);
		virtual ~FaceDetector(void);
		!FaceDetector(void);

		void SetImage( System::String ^ imgFileName );
		void SetClassifier( System::String ^ classifierFileName );
		List<System::Drawing::Rectangle> ^ DetectFaces();

};

}