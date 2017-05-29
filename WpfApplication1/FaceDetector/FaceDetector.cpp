#include "stdafx.h"
#include "FaceDetector.h"

namespace SNFaceCrop {

//============================================================
		FaceDetector::FaceDetector(void)
		{
			scale = 1;
			imageIsOpened = false;
			cascade = new CascadeClassifier();
			cascadeName = new cv::String();
			inputName= new cv::String();
			imageSize= new cv::Size();
			this->faces = gcnew List<System::Drawing::Rectangle>();
			//this->FacesExtended = gcnew List<System::Drawing::Rectangle>();

		}

//============================================================
		FaceDetector::~FaceDetector(void)
		{
			this->!FaceDetector();
		}

//============================================================
		FaceDetector::!FaceDetector(void)
		{
			delete cascade;
			delete cascadeName;
			delete inputName;
			delete imageSize;
		}

//============================================================
		void FaceDetector::marshalString ( System::String ^ s, cv::String * cvs ) {
			using namespace Runtime::InteropServices;
			const char* chars = 
					(const char*)(Marshal::StringToHGlobalAnsi(s)).ToPointer();
			//stdstring = chars;
			cvs->assign( chars );
			Marshal::FreeHGlobal(IntPtr((void*)chars));
		}

//============================================================
		List<System::Drawing::Rectangle> ^ FaceDetector::getListOfRectangle( vector<Rect> &rectVect )
		{
			List<System::Drawing::Rectangle> ^ rectList = gcnew List<System::Drawing::Rectangle>();
			System::Drawing::Rectangle r;
			for( vector<Rect>::const_iterator rect = rectVect.begin(); rect != rectVect.end(); rect++ )
			{
				r = System::Drawing::Rectangle(rect->x, rect->y, rect->width, rect->height);
				rectList->Add( r );
			}
			return rectList;
		}

//============================================================
		void FaceDetector::detectCascadeClassifier( Mat& img,
																 CascadeClassifier& cascade,
																 double scale,
																 vector<Rect>& detectedFaces
																 )
		{
			Mat gray, smallImg( cvRound (img.rows/scale), cvRound(img.cols/scale), CV_8UC1 );

			cvtColor( img, gray, CV_BGR2GRAY );
			resize( gray, smallImg, smallImg.size(), 0, 0, INTER_LINEAR );
			equalizeHist( smallImg, smallImg );

			cascade.detectMultiScale( smallImg, detectedFaces,
				1.1, 2, 0
				//|CV_HAAR_FIND_BIGGEST_OBJECT
				//|CV_HAAR_DO_ROUGH_SEARCH
				|CV_HAAR_SCALE_IMAGE,
				cv::Size(30, 30) );

			return;
		}

		//============================================================
		void FaceDetector::SetImage( System::String ^ imgFileName )
		{
			marshalString( imgFileName, this->inputName );

			this->faces->Clear();
		}

		//============================================================
		void FaceDetector::SetClassifier( System::String ^ classifierFileName )
		{
			marshalString( classifierFileName, this->cascadeName );

			System::Diagnostics::Trace::WriteLine( classifierFileName );
			
			if( !cascade->load( *(this->cascadeName) ) )
			{
				System::String ^ msg = "Failed loading classifier: " + gcnew System::String(this->cascadeName->c_str());
				throw gcnew ApplicationException(msg);
			}
		}

		//============================================================
		List<System::Drawing::Rectangle> ^ FaceDetector::DetectFaces()
		{
			Mat image;
			vector<Rect> faces;

			image = imread( *inputName, 1 );
			*imageSize = image.size();
			if( !image.empty() )
			{
				detectCascadeClassifier( image, *cascade, scale, faces );
			}

			this->faces->Clear();
			this->faces = getListOfRectangle( faces );
			return this->faces;
		}

}